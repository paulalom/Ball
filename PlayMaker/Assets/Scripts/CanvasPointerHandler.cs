using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasPointerHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 pointerDownAt = Vector3.negativeInfinity;
    public OnCourtObject pointerDownObject, pointerUpObject;
    float dragDistanceTolerance = 3f;

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        pointerDownObject = go.GetComponentInParent<OnCourtObject>() ?? go.GetComponent<OnCourtObject>();
        pointerDownAt = Input.mousePosition;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        pointerUpObject = go.GetComponentInParent<OnCourtObject>() ?? go.GetComponent<OnCourtObject>();
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }
    public bool IsNotDrag()
    {
        return Math.Abs((pointerDownAt - Input.mousePosition).sqrMagnitude) <= dragDistanceTolerance * dragDistanceTolerance;
    }
}
