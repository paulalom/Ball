using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayMakerObjectBuilder {
    
    public abstract OnCourtObject GetOnCourtObject(Dictionary<string, GameObject> prefabs);
    public abstract bool ValidateClick(Vector3 clickLocation, OnCourtObject clickedObject);
    public abstract bool ValidateDrag(Vector3 draggedFrom, Vector3 draggedTo, OnCourtObject dragStartObject, OnCourtObject dragEndObject);
}
