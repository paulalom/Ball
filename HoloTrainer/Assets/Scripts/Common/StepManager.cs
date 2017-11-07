using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepManager : MonoBehaviour {

    public int currentStep = 0;
    public bool showAllSteps = false;
    public Court court;
    public PlayerStep currentPlayerStep;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void ShowAllSteps()
    {
        showAllSteps = true;
        RefreshBoard();
    }

    /// <summary>
    /// SetStep called internally which updates the slider visual as well as the current step
    /// </summary>
    /// <param name="step"></param>
    public void SetCurrentStep(int step)
    {
        showAllSteps = false;
        currentStep = step;
        // Fixme, slow... change playerSteps datastructure
        currentPlayerStep = court.courtData.playerSteps[step].FirstOrDefault(x => x.playerId == court.courtData.humanPlayerId);
        RefreshBoard();
    }
    
    public void RefreshBoard()
    {
        if (showAllSteps)
        {
            SetAllOnCourtObjectsVisibility(true);
        }
        else
        {
            SetAllOnCourtObjectsVisibility(false);
            SetOnCourtObjectsVisibilityForStep(true, currentStep);
        }
    }
    
    void SetOnCourtObjectsVisibilityForStep(bool visible, int stepId)
    {
        foreach (PlayerStep player in court.courtData.playerSteps[stepId])
        {
            foreach (PlayerStepAction action in player.playerActions)
            {
                action.gameObject.SetActive(true);
            }
            player.gameObject.SetActive(true);
        }
    }

    void SetAllOnCourtObjectsVisibility(bool visible)
    {
        foreach (List<PlayerStep> step in court.courtData.playerSteps)
        {
            foreach (PlayerStep player in step)
            {
                foreach (PlayerStepAction action in player.playerActions)
                {
                    action.gameObject.SetActive(visible);
                }
                player.gameObject.SetActive(visible);
            }
        }
    }
}

