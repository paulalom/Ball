using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ActionSelecter activeActionSelecter;
    public PlayMakerManager playMakerManager;
    public List<ActionSelecter> selecters;
    public List<Sprite> OnCourtImages;
    public CanvasPointerHandler pointerHandler;
    public bool eraseOnClick = false;

    void Start()
    {
        foreach (ActionSelecter selecter in selecters)
        {
            selecter.Disselect();
        }
    }

    public void SetEraser()
    {
        eraseOnClick = true;
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
            // eraser is hackish, should change the way actionSelecter works to allow for this functionality
            if (eraseOnClick && Input.GetKeyUp(KeyCode.Mouse0) && pointerHandler.IsNotDrag())
            {
                playMakerManager.Erase(pointerHandler.pointerDownObject);
                eraseOnClick = false;
                pointerHandler.pointerDownObject = null;
                return;
            }

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
        if (!activeActionSelecter.objectBuilder.ValidateClick(clickLocation, clickedObject))
        {
            return;
        }
        playMakerManager.AddPlayer(clickLocation, activeActionSelecter.objectBuilder);
    }

    // need more accurate drag, want to be able to draw
    void ProcessDragInput(Vector3 draggedFrom, Vector3 draggedTo, OnCourtObject dragStartObject, OnCourtObject dragEndObject)
    { 
        if (!activeActionSelecter.objectBuilder.ValidateDrag(draggedFrom, draggedTo, dragStartObject, dragEndObject))
        {
            return;
        }
        playMakerManager.AddPlayerAction(draggedFrom, draggedTo, dragStartObject.GetComponent<Player>(), (PlayerActionBuilder)activeActionSelecter.objectBuilder);
    }
}
