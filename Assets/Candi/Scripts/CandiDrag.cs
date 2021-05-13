using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;



public class CandiDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    public GameObject candiParticle;
    private CanvasGroup canvasGroup;
    private new Rigidbody2D rigidbody2D;

    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;

    public bool followCursor { get; set; } = true;
    public bool canDrag { get; set; } = true;

    public Vector2 tempPosition;

    private bool beingHeld = false;
    public bool horizontalState;
    private float groundY = -219f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
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
        if (rectTransform.anchoredPosition.x > 1200f || rectTransform.anchoredPosition.x < -1200f)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x > 1200f ? 1200f : -1200f, rectTransform.anchoredPosition.y);
        }
        if (rectTransform.anchoredPosition.y < (groundY - 1))
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, groundY) ;
        }

    }

    private void particleCreateAndDestroy()
    {
        GameObject particle = Instantiate(candiParticle, transform.position, candiParticle.transform.rotation) as GameObject;
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(particle, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        particleCreateAndDestroy();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        canvasGroup.alpha = .5f;
        rigidbody2D.gravityScale = 0f;
        canvasGroup.blocksRaycasts = false;

        OnBeginDragHandler?.Invoke(eventData);
        beingHeld = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        OnDragHandler?.Invoke(eventData);
        if (followCursor)
        {
            rectTransform.anchoredPosition += eventData.delta / (0.75f / (965f / Screen.width));
            tempPosition = rectTransform.anchoredPosition;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

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
                canDrag = false;
                dropArea.Drop(this);
                OnEndDragHandler?.Invoke(eventData, true);
                return;
            }
        }

        rigidbody2D.gravityScale = 150f;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        particleCreateAndDestroy();
        OnEndDragHandler?.Invoke(eventData, false);
        beingHeld = false;

    }

    //public void OnInitializePotentialDrag(PointerEventData eventData)
    //{
    //    StartPosition = rectTransform.anchoredPosition;
    //}
}
