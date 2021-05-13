using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCandiRectangle : CandiCondition
{
    public override bool Check(CandiDrag draggable)
    {
        return draggable.GetComponent<CandiRectangle>() != null;
    }
}
