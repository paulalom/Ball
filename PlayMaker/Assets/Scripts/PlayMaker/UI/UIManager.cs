﻿using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ActionSelecter activeActionSelecter;
    public PlayMakerManager playMakerManager;
    public List<ActionSelecter> selecters;
    public List<Sprite> OnCourtImages;
    public CanvasPointerHandler pointerHandler;

    void Start()
    {
        foreach (ActionSelecter selecter in selecters)
        {
            selecter.Disselect();
        }
    }

    public void SetUISelection(ActionSelecter selecter)
    {
        if (activeActionSelecter != null)
        {
            activeActionSelecter.Disselect();
        }
        activeActionSelecter = selecter;
        activeActionSelecter.Select();
    }

    public void Update()
    {
        if (activeActionSelecter != null)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) && pointerHandler.IsNotDrag())
            {
                ProcessClickInput(Input.mousePosition, pointerHandler.pointerDownObject);
                pointerHandler.pointerDownObject = null;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                ProcessDragInput(pointerHandler.pointerDownAt, Input.mousePosition, pointerHandler.pointerDownObject, pointerHandler.pointerUpObject);
                pointerHandler.pointerDownObject = null;
            }
        }
    }
    
    void ProcessClickInput(Vector3 clickLocation, OnCourtObject clickedObject)
    {
        if (activeActionSelecter.action is AddPlayer && clickedObject != playMakerManager.court)
        {
            return;
        }
        playMakerManager.DoClickAction(clickLocation, activeActionSelecter.action);
    }

    // need more accurate drag, want to be able to draw
    void ProcessDragInput(Vector3 draggedFrom, Vector3 draggedTo, OnCourtObject dragStartObject, OnCourtObject dragEndObject)
    { 
        if (dragStartObject == null || dragStartObject.GetComponent<Player>() == null)
        {
            return;
        }

        playMakerManager.DoDragAction(draggedFrom, draggedTo, dragStartObject.GetComponent<Player>(), activeActionSelecter.action);
    }

}
