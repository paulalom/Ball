﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepAdder {

    public int playerId;
    public UIManager uiManager;

    public OnCourtObject GetOnCourtObject(Dictionary<string, GameObject> prefabs)
    {
        GameObject go = GameObject.Instantiate(prefabs[typeof(PlayerStep).ToString()]);
        PlayerStep player = go.GetComponent<PlayerStep>();
        player.playerId = playerId;
        player.text.text = ((playerId % 5) + 1).ToString();
        player.spriteRenderer.sprite = uiManager.onCourtSprites[(int)(playerId / 5)];
        // move text above sprite
        player.text.transform.localPosition += new Vector3(0, 0.01f, 0);
        return player;
    }
}
