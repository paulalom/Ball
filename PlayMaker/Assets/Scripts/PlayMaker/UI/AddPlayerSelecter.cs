using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerSelecter : ActionSelecter
{
    private void Start()
    {
        objectBuilder = new PlayerBuilder(0);
    }

    public override void OnValueChanged(int newValueIndex)
    {
        objectBuilder = new PlayerBuilder(newValueIndex);
    }
}
