using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;



public class CandiDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;

    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;

    public bool followCursor { get; set; } = true;
    public bool canDrag { get; set; } = true;

    public Vector2 tempPosition;

    private bool beingHeld = false;
    public bool horizontalState;
    public GameObject candiParticle;


    private void particleCreateAndDestroy()
    {
        GameObject exp1 = Instantiate(candiParticle, transform.position, candiParticle.transform.rotation) as GameObject;
        exp1.GetComponent<ParticleSystem>().Play();
        Destroy(exp1, 2f);
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        particleCreateAndDestroy();
    }
    private void Update()
    {

        if (beingHeld)
        {
            if (Input.GetMouseButtonUp(1))
            {
                transform.Rotate(0f, 0f, -90f);
                if(Mathf.Abs(transform.rotation.z) == 90) horizontalState = true;
                else horizontalState = false;
            }
            if (Input.mousePosition.x > Screen.width - CameraHandler.edgeScreen || Input.mousePosition.x < CameraHandler.edgeScreen)
            {
                Vector2 tempCamPos = CameraHandler.camPos;
                tempCamPos.y = tempPosition.y;
                if (Input.mousePosition.x > Screen.width - CameraHandler.edgeScreen) tempCamPos.x += 640 - (Screen.width - Input.mousePosition.x);
                else tempCamPos.x -= 640 - (Input.mousePosition.x);
                rectTransform.anchoredPosition = tempCamPos;
            }
        }


    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return;
        }
        OnBeginDragHandler?.Invoke(eventData);
        beingHeld = true;
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (!canDrag)
        {
            return;
        }
        OnDragHandler?.Invoke(eventData);
        if (followCursor)
        {
            rectTransform.anchoredPosition += eventData.delta / (0.75f / (965f / Screen.width));
            tempPosition = rectTransform.anchoredPosition;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        particleCreateAndDestroy();
        if (!canDrag)
        {
            return;
        }

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        CandiArea dropArea = null;

        foreach (var result in results)
        {
            dropArea = result.gameObject.GetComponent<CandiArea>();

            if (dropArea != null)
            {
                break;
            }
        }

        if (dropArea != null)
        {
            if (dropArea.Accepts(this))
            {
                Debug.Log("Drop Accept");
                canDrag = false;
                dropArea.Drop(this);
                OnEndDragHandler?.Invoke(eventData, true);
                return;
            }
        }

        // rectTransform.anchoredPosition = StartPosition;
        OnEndDragHandler?.Invoke(eventData, false);
        beingHeld = false;

    }

    //public void OnInitializePotentialDrag(PointerEventData eventData)
    //{
    //    StartPosition = rectTransform.anchoredPosition;
    //}
}
