using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLSquare : GuideLine
{
    protected override void Awake()
    {
        candiDirectionAsk = false;

        base.Awake();
        DropArea.DropConditions.Add(new IsCandiSquare());
    }
}
