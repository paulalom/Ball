﻿using UnityEngine;
using System.Collections.Generic;

public class PlayMakerManager : MonoBehaviour {
    
    public ScreenManager screenManager;
    public Court court;
    public List<int> playersOnBoard = new List<int>();
    public Dictionary<int, Tree> playerPlays = new Dictionary<int, Tree>();

    public List<string> inspectorPrefabNames;
    public List<GameObject> inspectorPrefabObjects;
    public Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < inspectorPrefabObjects.Count; i++)
        {
            prefabs.Add(inspectorPrefabNames[i], inspectorPrefabObjects[i]);
        }
    }
	
    public void DoClickAction(Vector3 clickLocation, PlayMakerAction action)
    {
        OnCourtObject obj = action.GetOnCourtObject(prefabs);
        obj.GetComponent<RectTransform>().position = clickLocation;
        obj.transform.SetParent(court.transform, true);
    }

    public void DoDragAction(Vector3 startPoint, Vector3 endPoint, Player dragActionPlayer, PlayMakerAction action)
    {

    }

    public void AddPlay(Vector3 startPoint, Vector3 endPoint, Player playPlayer, PlayMakerAction play)
    {
        //PlayStep playStep = PlayStepFactory.GetPlayStep(play, startPoint, endPoint);
        //playPlayer.playSteps.Add();
    }

    public void MovePlayer(Vector3 location, PlayMakerAction action)
    {

    }
}
