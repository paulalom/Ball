using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayer : PlayMakerAction {

    int playerId;
    UIManager uiManager;

    public AddPlayer(int playerId)
    {
        this.playerId = playerId;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public override OnCourtObject GetOnCourtObject(Dictionary<string, GameObject> prefabs)
    {
        GameObject go = GameObject.Instantiate(prefabs[typeof(Player).ToString()]);
        Player player = go.GetComponent<Player>();
        player.text.text = ((playerId % 5) + 1).ToString();
        player.image.sprite = playerId <= 5 ? uiManager.OnCourtImages[0] : uiManager.OnCourtImages[1];
        return player;
    }
}
