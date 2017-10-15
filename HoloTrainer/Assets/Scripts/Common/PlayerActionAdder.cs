using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionAdder {

    public PlayerActionType playStepId;
    public UIManager uiManager;

    public OnCourtObject GetOnCourtObject(Dictionary<string, GameObject> prefabs)
    {
        GameObject go = GameObject.Instantiate(prefabs[typeof(PlayerAction).ToString()]);
        PlayerAction playStep = go.GetComponent<PlayerAction>();
        playStep.stepType = playStepId;
        playStep.spriteRenderer.sprite = uiManager.onCourtSprites[(int)playStepId + 2];
        return playStep;
    }
}
