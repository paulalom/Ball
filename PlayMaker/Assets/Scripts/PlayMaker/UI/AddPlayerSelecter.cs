using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerSelecter : ActionSelecter
{
    private void Start()
    {
        action = new PlayerBuilder(0);
    }

    public override void OnValueChanged(int newValueIndex)
    {
        action = new PlayerBuilder(newValueIndex);
    }
}
