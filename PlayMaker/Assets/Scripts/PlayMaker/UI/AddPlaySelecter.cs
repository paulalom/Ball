using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlaySelecter : ActionSelecter
{
    private void Start()
    {
        objectBuilder = new PlayerActionBuilder(0);
    }

    public override void OnValueChanged(int newValueIndex)
    {
        objectBuilder = new PlayerActionBuilder(newValueIndex);
    }
}