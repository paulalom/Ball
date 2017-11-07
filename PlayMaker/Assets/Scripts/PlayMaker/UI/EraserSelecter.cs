using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserSelecter : ActionSelecter
{
    public override void OnValueChanged(int newValueIndex)
    {
        throw new NotImplementedException();
    }

    void Start()
    {
        selecterType = ActionSelecterType.Remove;
    }
}
