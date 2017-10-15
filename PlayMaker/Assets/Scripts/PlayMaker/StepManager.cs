using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepManager : MonoBehaviour {

    public int currentStep = 0;
    public bool showAllSteps = false;
    public Slider stepSlider;
    public Court court;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void StartStepPlaythrough()
    {

    }

    public void PauseStepPlaythrough()
    {

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
        SetCurrentStep((float)step);
        stepSlider.value = step;
    }

    /// <summary>
    /// SetStep called from the Step Slider
    /// </summary>
    /// <param name="stepSliderPosition">The position of the slider bar on the slider from 0-1</param>
    public void SetCurrentStep(float stepSliderPosition)
    {
        showAllSteps = false;
        currentStep = (int)stepSliderPosition;
        RefreshBoard();
    }

    public void AddStep(bool switchToStep)
    {
        court.courtData.playerSteps.Add(new List<Player>());
        int totalSteps = court.courtData.playerSteps.Count;

        stepSlider.maxValue = totalSteps - 1;
        Text endText = stepSlider.GetComponentsInChildren<Text>().Where(x => x.name == "EndValueText").FirstOrDefault();
        endText.text = "" + (totalSteps - 1);

        foreach (Player player in court.courtData.playerSteps[totalSteps - 2])
        {
            ObjectReference.playMakerManager.AddPlayer(player.transform.position, new PlayerBuilder(player.playerTypeId), totalSteps - 1);
        }

        if (switchToStep)
        {
            SetCurrentStep(court.courtData.playerSteps.Count - 1);
        }
    }

    public void RemoveStep()
    {
        court.courtData.playerSteps.RemoveAt(court.courtData.playerSteps.Count - 1);
        stepSlider.maxValue = court.courtData.playerSteps.Count - 1;
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
        foreach (Player player in court.courtData.playerSteps[stepId])
        {
            foreach (PlayerAction action in player.playerActions)
            {
                action.gameObject.SetActive(true);
            }
            player.gameObject.SetActive(true);
        }
    }

    void SetAllOnCourtObjectsVisibility(bool visible)
    {
        foreach (List<Player> step in court.courtData.playerSteps)
        {
            foreach (Player player in step)
            {
                foreach (PlayerAction action in player.playerActions)
                {
                    action.gameObject.SetActive(visible);
                }
                player.gameObject.SetActive(visible);
            }
        }
    }
}

