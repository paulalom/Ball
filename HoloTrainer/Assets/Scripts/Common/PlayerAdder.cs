using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdder {

    public int playerId;
    public UIManager uiManager;

    public OnCourtObject GetOnCourtObject(Dictionary<string, GameObject> prefabs)
    {
        GameObject go = GameObject.Instantiate(prefabs[typeof(Player).ToString()]);
        Player player = go.GetComponent<Player>();
        player.playerId = playerId;
        player.text.text = ((playerId % 5) + 1).ToString();
        player.spriteRenderer.sprite = uiManager.onCourtSprites[(int)(playerId / 5)];
        return player;
    }
}
