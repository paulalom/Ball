using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {

    public UIManager uiManager;
    public StepManager stepManager;
    public Court court;
    public VirtualPlayerManager virtualPlayerManager;
    public string pathToPlaySaveFile;

    public List<string> inspectorPrefabNames;
    public List<GameObject> inspectorPrefabObjects;
    public Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    public Player humanPlayer;

    bool isAwake = false;

    void Awake()
    {
        string saveText = GetSaveText();

        for (int i = 0; i < inspectorPrefabObjects.Count; i++)
        {
            prefabs.Add(inspectorPrefabNames[i], inspectorPrefabObjects[i]);
        }

        ValidateSaveFile(saveText);
        court.courtData = CourtData.FromString(saveText, court, this);
        stepManager.SetCurrentStep(0);
        virtualPlayerManager.court = court;
        virtualPlayerManager.SpawnVirtualPlayers(prefabs);
        humanPlayer.parent.transform.position = new Vector3(stepManager.currentPlayerStep.transform.position.x, 
            1.6f, 
            stepManager.currentPlayerStep.transform.position.z);
        isAwake = true;
    }


    string GetSaveText()
    {
        return "0#475,893#0@-142,-343:0@-144,-338,0=-143,-258,0|1@150,-321:0@155,-310,0=155,-177,0|2@-138,141:0@-128,137,0=-134,10,0&0@-143,-258:0@-137,-260,0=-43,-259,0|1@155,-177:0@145,-176,0=156,-42,0|2@-134,10:0@-122,10,0=1,5,0&0@-43,-259:0@-39,-249,0=-42,-156,0|1@156,-42:0@145,-27,0=151,93,0|2@1,5:0@10,0,0=34,-141,0&0@-42,-156:0@-48,-160,0=-151,-159,0|1@151,93:0@149,95,0=151,217,0|2@34,-141:0@22,-148,0=-66,-244,0&0@-151,-159:|1@151,217:0@151,217,0=154,-180,0|2@-66,-244:0@-66,-244,0=-140,12,0";
        //TextAsset saveFile = Resources.Load(pathToPlaySaveFile, typeof(TextAsset)) as TextAsset;
        //return saveFile.text;
    }

    bool ValidateSaveFile(string fileText)
    {
        if (FindFirstNotAny(fileText, Court.validSaveCourtCharacters) != null)
        {
            throw new System.Exception("Invalid characters in save file!");
        }
        return true;
    }



	// Update is called once per frame
	void Update () {
        if (!isAwake)
        {
            Awake();
        }
		if (stepManager.currentStep < court.courtData.playerSteps.Count - 1 && IsPlayerStepCompleted(stepManager.currentPlayerStep))
        {
            stepManager.SetCurrentStep(stepManager.currentStep + 1);   
        }

        if (stepManager.currentPlayerStep != null && stepManager.currentPlayerStep.playerActions.Count > 0)
        {
            /*
            uiManager.directionChevron.gameObject.SetActive(true);
            if (stepManager.currentPlayerStep.playerActions[0].isStarted)
            {
                uiManager.directionChevron.pointTo = stepManager.currentPlayerStep.playerActions[0].end;
            }
            else
            {
                uiManager.directionChevron.pointTo = stepManager.currentPlayerStep.playerActions[0].start;
            }*/
        }
        else
        {
            //uiManager.directionChevron.gameObject.SetActive(false);
        }
    }

    bool IsPlayerStepCompleted(PlayerStep player)
    {
        bool stepCompleted = true;
        foreach (PlayerStepAction action in player.playerActions)
        {
            if (!action.IsStarted(humanPlayer) || !action.IsDone(humanPlayer))
            {
                stepCompleted = false;
            }
        }
        return stepCompleted;
    }


    public static string FindFirstNotAny(string value, params char[] charset)
    {
        string firstInvalid = value.TrimStart(charset);
        return firstInvalid.Length > 0 ? firstInvalid : null;
    }
}
