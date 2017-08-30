using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddPlayStep : PlayMakerAction
{
    public PlayStepType playId;
    UIManager uiManager;

    public AddPlayStep(int playId)
    {
        this.playId = (PlayStepType)playId;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public override OnCourtObject GetOnCourtObject(Dictionary<string, GameObject> prefabs)
    {
        GameObject go = GameObject.Instantiate(prefabs[typeof(PlayStep).ToString()]);
        PlayStep playStep = go.GetComponent<PlayStep>();
        playStep.image.sprite = uiManager.OnCourtImages[(int)playId + 2];
        return playStep;
    }
}
