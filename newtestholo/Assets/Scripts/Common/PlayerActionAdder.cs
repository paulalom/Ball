using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepActionAdder {

    public PlayerActionType playStepId;
    public UIManager uiManager;

    public OnCourtObject GetOnCourtObject(Dictionary<string, GameObject> prefabs)
    {
        string prefabName = Enum.GetName(typeof(PlayerActionType), playStepId) + "StepAction";
        Debug.Log(prefabName);
        GameObject go = GameObject.Instantiate(prefabs[prefabName]);
        PlayerStepAction playStep = go.GetComponent<PlayerStepAction>();
        playStep.stepType = playStepId;
        playStep.spriteRenderer.sprite = uiManager.onCourtSprites[(int)playStepId + 2];
        return playStep;
    }
}
