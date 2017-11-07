using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public UIManager uiManager;
    public StepManager stepManager;
    public Court court;
    public string pathToPlaySaveFile;

    public List<string> inspectorPrefabNames;
    public List<GameObject> inspectorPrefabObjects;
    public Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    public Player humanPlayer;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < inspectorPrefabObjects.Count; i++)
        {
            prefabs.Add(inspectorPrefabNames[i], inspectorPrefabObjects[i]);
        }

        string savedPlayString = "";

        foreach (string line in MyFileReader.Read(pathToPlaySaveFile))
        {
            Debug.Log(line);
            if (FindFirstNotAny(line, Court.validSaveCourtCharacters) != null)
            {
                throw new System.Exception("Invalid characters in save file!");
            }

            savedPlayString = line;
        }

        humanPlayer.eyes.fieldOfView = 90f;

        court.courtData = CourtData.FromString(savedPlayString, court, this);
        stepManager.SetCurrentStep(0);
        humanPlayer.transform.position = new Vector3(stepManager.currentPlayerStep.transform.position.x, 
            humanPlayer.transform.position.y, 
            stepManager.currentPlayerStep.transform.position.z);
    }

	// Update is called once per frame
	void Update () {
		if (stepManager.currentStep < court.courtData.playerSteps.Count - 1 && IsPlayerStepCompleted(stepManager.currentPlayerStep))
        {
            stepManager.SetCurrentStep(stepManager.currentStep + 1);   
        }

        if (stepManager.currentPlayerStep != null && stepManager.currentPlayerStep.playerActions.Count > 0)
        {
            uiManager.directionChevron.gameObject.SetActive(true);
            if (stepManager.currentPlayerStep.playerActions[0].isStarted)
            {
                uiManager.directionChevron.pointTo = stepManager.currentPlayerStep.playerActions[0].end;
            }
            else
            {
                uiManager.directionChevron.pointTo = stepManager.currentPlayerStep.playerActions[0].start;
            }
        }
        else
        {
            uiManager.directionChevron.gameObject.SetActive(false);
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
