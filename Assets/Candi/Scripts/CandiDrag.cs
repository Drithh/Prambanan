using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;



public class CandiDrag : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;

    public RectTransform rectTransform;

    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;

    public bool FollowCursor { get; set; } = true;
    public Vector3 StartPosition;
    public bool CanDrag { get; set; } = true;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!CanDrag)
        {
            return;
        }
        OnBeginDragHandler?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(CameraHandler.camPos.x);
        Debug.Log(rectTransform.anchoredPosition);

        if (!CanDrag)
        {
            return;
        }
        OnDragHandler?.Invoke(eventData);
        if (FollowCursor)
        {
            rectTransform.anchoredPosition += eventData.delta / 0.74f;
        }
        // rectTransform.anchoredPosition += (Vector2)CameraHandler.camPos;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (!CanDrag)
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
            Debug.Log("dropArea Full");
        }

        if (dropArea != null)
        {
            if (dropArea.Accepts(this))
            {
                Debug.Log("Drop Accept");
                CanDrag = false;
                dropArea.Drop(this);
                OnEndDragHandler?.Invoke(eventData, true);
                return;
            }
        }

        rectTransform.anchoredPosition = StartPosition;
        OnEndDragHandler?.Invoke(eventData, false);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        StartPosition = rectTransform.anchoredPosition;
    }
}
