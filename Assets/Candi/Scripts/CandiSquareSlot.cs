using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandiSquareSlot : CandiSlot
{
    protected override void Awake()
    {
        base.Awake();
        DropArea.DropConditions.Add(new IsCandiSquare());
    }
}
