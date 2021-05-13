using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuideLine : MonoBehaviour
{
	public static int totalCandi = 0;
	protected CandiArea DropArea;
	protected bool candiDirectionAsk;
	protected bool candiDirectionAskState;

	protected virtual void Awake()
	{
		DropArea = GetComponent<CandiArea>() ?? gameObject.AddComponent<CandiArea>();
		DropArea.OnDropHandler += OnItemDropped;
	}

	private void OnItemDropped(CandiDrag draggable)
	{
		if (candiDirectionAsk && candiDirectionAskState != draggable.horizontalState) return;

		totalCandi++;
		draggable.transform.position = transform.position;
		Destroy(gameObject);
	}
}
