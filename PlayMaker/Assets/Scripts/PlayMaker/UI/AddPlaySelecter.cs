using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlaySelecter : ActionSelecter
{
    public override void OnValueChanged(int newValueIndex)
    {
        action = new AddPlayStep(newValueIndex);
    }
}
