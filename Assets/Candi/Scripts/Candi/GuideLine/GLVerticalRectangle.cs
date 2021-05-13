using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLVerticalRectangle : GuideLine
{
    protected override void Awake()
    {
        base.candiDirectionAsk = true;
        base.candiDirectionAskState = false;
        base.Awake();
        DropArea.DropConditions.Add(new IsCandiRectangle());
    }
}
