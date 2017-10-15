using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ActionSelecterType
{
    AddPlay,
    AddPlayer,
    Reposition,
    Remove
}
public abstract class ActionSelecter : MonoBehaviour {

    public PlayMakerObjectBuilder action;
    public ActionSelecterType selecterType;
    public Image selectionImage;
    
    public void Select()
    {
        selectionImage.enabled = true;
    }

    public void Disselect()
    {
        selectionImage.enabled = false;
    }

    public abstract void OnValueChanged(int newValueIndex);
}
