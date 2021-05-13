using System.Collections.Generic;
using System;
using UnityEngine;

public class CandiArea : MonoBehaviour
{
	public List<CandiCondition> DropConditions = new List<CandiCondition>();
	public event Action<CandiDrag> OnDropHandler;

	public bool Accepts(CandiDrag draggable)
	{
		return DropConditions.TrueForAll(cond => cond.Check(draggable));
	}

	public void Drop(CandiDrag draggable)
	{
		OnDropHandler?.Invoke(draggable);
	}
}
