using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CandiSlot : MonoBehaviour
{
	private static int totalCandi = 0;
	protected CandiArea DropArea;


    protected virtual void Awake()
	{
		DropArea = GetComponent<CandiArea>() ?? gameObject.AddComponent<CandiArea>();
		DropArea.OnDropHandler += OnItemDropped;
	}

	private void OnItemDropped(CandiDrag draggable)
	{
		totalCandi++;
        Debug.Log(totalCandi);
		draggable.transform.position = transform.position;
	}
}
