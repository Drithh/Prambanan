using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCandiSquare : CandiCondition
{
    public override bool Check(CandiDrag draggable)
    {
        return draggable.GetComponent<CandiSquare>() != null;
    }
}
