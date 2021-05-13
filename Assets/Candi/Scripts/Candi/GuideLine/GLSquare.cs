using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLSquare : GuideLine
{
    protected override void Awake()
    {
        base.candiDirectionAsk = true;
        base.candiDirectionAskState = true;
        base.Awake();
        DropArea.DropConditions.Add(new IsCandiSquare());
    }
}
