using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerSelecter : ActionSelecter
{
    public override void OnValueChanged(int newValueIndex)
    {
        action = new AddPlayer(newValueIndex);
    }
}
