using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CandiDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    public GameObject candiParticle;
    private CanvasGroup canvasGroup;
    private new Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;

    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;

    public bool followCursor { get; set; } = true;
    public bool canDrag { get; set; } = true;

    public Vector2 tempPosition;

    private bool leftBeingHeld = false;
    private bool rightBeingHeld = false;

    public bool horizontalState;
    private float groundY = -226f;
    private float screenScale = (965f / Screen.width);
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    private void Update()
    {

        if (leftBeingHeld)
        {
            if (Input.GetMouseButtonDown(1)) rightBeingHeld = true;
            else if (Input.GetMouseButtonUp(1)) rightBeingHeld = false;
            if (rightBeingHeld) transform.Rotate(0f, 0f, -0.8f);

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
            //rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, groundY);
        }

    }

    private void particleCreateAndDestroy()
    {
        GameObject particle = Instantiate(candiParticle, transform.position, candiParticle.transform.rotation) as GameObject;
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(particle, 1f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Math.Abs((tempPosition.x + 360) - (rectTransform.anchoredPosition.x + 360)) > 50 || Math.Abs((tempPosition.y + 360) - (rectTransform.anchoredPosition.y + 360)) > 50) particleCreateAndDestroy();
        tempPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        canvasGroup.alpha = .5f;
        rigidbody2D.gravityScale = 0f;
        canvasGroup.blocksRaycasts = false;

        OnBeginDragHandler?.Invoke(eventData);
        leftBeingHeld = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        rigidbody2D.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        OnDragHandler?.Invoke(eventData);
        if (followCursor)
        {
            rectTransform.anchoredPosition += eventData.delta / (0.75f / screenScale);
            tempPosition = rectTransform.anchoredPosition;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        OnEndDragHandler?.Invoke(eventData, false);
        leftBeingHeld = false;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        CandiArea dropArea = null;

        foreach (var result in results)
        {
            dropArea = result.gameObject.GetComponent<CandiArea>();
            if (dropArea != null) break;
        }

        if (dropArea != null && dropArea.Accepts(this))
        {
            int TransformX = (int) rigidbody2D.rotation;
            if(TransformX < 0) TransformX = (TransformX - 45) / 90 * 90;
            else  TransformX = (TransformX + 45) / 90 * 90;

            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, TransformX));

            if (Mathf.Abs(TransformX) / 90 % 2 == 1) horizontalState = false;
            else horizontalState = true;

            int lastTotalCandi = GuideLine.totalCandi;
            dropArea.Drop(this);
            OnEndDragHandler?.Invoke(eventData, true);

            if (lastTotalCandi != GuideLine.totalCandi)
            {
                rigidbody2D.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                canDrag = false;
                StartCoroutine(Waiter());
                transform.SetSiblingIndex(0);
                return;
            }
        }
        rigidbody2D.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.gravityScale = 150f;

    }

    IEnumerator Waiter()
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        for (float i = 1; i < 1.25f; i += 0.01f)
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.003f);

        }
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        particleCreateAndDestroy();
    }

}
