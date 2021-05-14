using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLHorizontalRectangle : GuideLine
{

    protected override void Awake()
    {
        candiDirectionAsk = true;
        candiDirectionAskState = true;

        base.Awake();
        DropArea.DropConditions.Add(new IsCandiRectangle());
    }
}
