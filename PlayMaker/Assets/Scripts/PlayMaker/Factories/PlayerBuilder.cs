using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilder : PlayMakerObjectBuilder {

    public int playerTypeId;
    UIManager uiManager;

    public PlayerBuilder(int playerId)
    { 
        this.playerTypeId = playerId;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public override OnCourtObject GetOnCourtObject(Dictionary<string, GameObject> prefabs)
    {
        GameObject go = GameObject.Instantiate(prefabs[typeof(Player).ToString()]);
        Player player = go.GetComponent<Player>();
        player.playerTypeId = playerTypeId;
        player.text.text = ((playerTypeId % 5) + 1).ToString();
        player.image.sprite = uiManager.OnCourtImages[(int)(playerTypeId / 5)];
        return player;
    }

    public override bool ValidateClick(Vector3 clickLocation, OnCourtObject clickedObject)
    {
        if (clickedObject == null || clickedObject.GetComponent<Court>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
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
