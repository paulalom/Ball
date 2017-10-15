using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlaySelecter : ActionSelecter
{
    private void Start()
    {
        action = new PlayerActionBuilder(0);
    }

    public override void OnValueChanged(int newValueIndex)
    {
        action = new PlayerActionBuilder(newValueIndex);
    }
}