using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerActionBuilder : PlayMakerObjectBuilder
{
    public PlayerActionType playerActionId;
    UIManager uiManager;

    public PlayerActionBuilder(int actionId)
    {
        this.playerActionId = (PlayerActionType)actionId;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public override OnCourtObject GetOnCourtObject(Dictionary<string, GameObject> prefabs)
    {
        GameObject go = GameObject.Instantiate(prefabs[typeof(PlayerAction).ToString()]);
        PlayerAction playerAction = go.GetComponent<PlayerAction>();
        playerAction.actionType = playerActionId;
        playerAction.image.sprite = uiManager.OnCourtImages[(int)playerActionId + 2];
        return playerAction;
    }
    
    public override bool ValidateClick(Vector3 clickLocation, OnCourtObject clickedObject)
    {
        return false;
    }

    public override bool ValidateDrag(Vector3 draggedFrom, Vector3 draggedTo, OnCourtObject dragStartObject, OnCourtObject dragEndObject)
    {
        if (dragStartObject == null || dragStartObject.GetComponent<Player>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
