using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

public class PlayMakerManager : MonoBehaviour {
    
    public ScreenManager screenManager;
    public Court court;

    public StepManager stepManager;
    public List<string> inspectorPrefabNames;
    public List<GameObject> inspectorPrefabObjects;
    
    public Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        ObjectReference.playMakerManager = this;
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < inspectorPrefabObjects.Count; i++)
        {
            prefabs.Add(inspectorPrefabNames[i], inspectorPrefabObjects[i]);
        }
        court.courtData.playMakerManager = this;
    }
	
    public void AddPlayer(Vector3 clickLocation, PlayMakerObjectBuilder builder)
    {
        AddPlayer(clickLocation, (PlayerBuilder)builder, stepManager.currentStep);
    }

    public void AddPlayer(Vector3 clickLocation, PlayMakerObjectBuilder builder, int stepId)
    {
        Player player = (Player)builder.GetOnCourtObject(prefabs);

        Player currentStepPlayer = GetStepPlayer(stepId, player.playerTypeId);

        // We already have a player of this type on the court. Move it to the clicklocation.
        if (currentStepPlayer != null)
        {
            MovePlayerAndPreserveActionEndpoints(currentStepPlayer, clickLocation);
            int curStepId = stepId;

            // Check next step to see if we need to add or move again.
            if (stepId < court.courtData.playerSteps.Count - 1 && currentStepPlayer.playerActions.Any(x => x.isMoveAction) == false)
            {
                AddPlayer(clickLocation, builder, stepId + 1);
            }
        }
        else // new player type, add to current and all future steps
        {
            player.GetComponent<RectTransform>().position = clickLocation;
            player.transform.SetParent(court.transform, true);
            court.AddPlayer(stepId, player);
            
            // Check next step to see if we need to add or move again.
            if (stepId < court.courtData.playerSteps.Count - 1)
            {
                AddPlayer(clickLocation, builder, stepId + 1);
            }
        }
    }
    
    public void AddPlayerAction(Vector3 startPoint, Vector3 endPoint, Player stepPlayer, PlayerActionBuilder playerActionBuilder)
    {
        AddPlayerAction(startPoint, endPoint, stepPlayer, playerActionBuilder, stepManager.currentStep);
    }

    public void AddPlayerAction(Vector3 startPoint, Vector3 endPoint, Player stepPlayer, PlayerActionBuilder playerActionBuilder, int stepId)
    {
        PlayerAction playerAction = (PlayerAction)playerActionBuilder.GetOnCourtObject(prefabs);
        playerAction.InitPlayerAction(startPoint, endPoint, stepPlayer);

        ClearPreviousAction(stepPlayer, playerAction);
        stepPlayer.playerActions.Add(playerAction);
        if (stepId == court.courtData.playerSteps.Count - 1)
        {
            stepManager.AddStep(false);
        }
        if (playerAction.isMoveAction)
        {
            AddPlayer(endPoint, new PlayerBuilder(stepPlayer.playerTypeId), stepId + 1);
            stepManager.RefreshBoard();
        }
    }

    void ClearPreviousAction(Player player, PlayerAction playerAction)
    {
        PlayerAction prevAction;
        if (playerAction.isMoveAction)
        {
            prevAction = GetPlayerMoveAction(player);
        }
        else
        {
            prevAction = GetPlayerActionByType(player, playerAction.actionType);
        }

        if (prevAction != null)
        {
            player.playerActions.Remove(prevAction);
            Destroy(prevAction.gameObject);
        }
    }

    void MovePlayerAndPreserveActionEndpoints(Player player, Vector3 targetPos)
    {
        List<Vector3> actionEndpoints = new List<Vector3>();
        foreach (PlayerAction action in player.playerActions)
        {
            actionEndpoints.Add(action.end);
        }
        player.transform.position = targetPos;

        for (int i = 0; i < actionEndpoints.Count; i++)
        {
            player.playerActions[i].InitPlayerAction(targetPos, actionEndpoints[i], player);
        }
    }

    Player GetStepPlayer(int stepId, int typeId)
    {
        return court.courtData.playerSteps[stepId].FirstOrDefault(x => x.playerTypeId == typeId);
    }
    PlayerAction GetPlayerMoveAction(Player player)
    {
        return player.playerActions.FirstOrDefault(x => x.isMoveAction);
    }
    PlayerAction GetPlayerActionByType(Player player, PlayerActionType actionType)
    {
        return player.playerActions.FirstOrDefault(x => x.actionType == actionType);
    }

    public void Erase(OnCourtObject objectToErase)
    {
        if (objectToErase.GetType() == typeof(Player))
        {
            court.courtData.playerSteps[stepManager.currentStep].Remove((Player)objectToErase);
            Destroy(objectToErase.gameObject);
        }
        else if (objectToErase.GetType() == typeof(PlayerAction))
        {
            PlayerAction action = (PlayerAction)objectToErase;
            action.owner.playerActions.Remove(action);
            Destroy(objectToErase.gameObject);
        }
    }

    public void LoadCourt(string courtString)
    {
        foreach (List<Player> stepPlayers in court.courtData.playerSteps)
        {
            foreach (Player player in stepPlayers)
            {
                Destroy(player.gameObject);
            }
        }
        court.courtData.playerSteps.Clear();
        court.courtData = CourtData.FromString(courtString, court, this);
        stepManager.stepSlider.maxValue = court.courtData.playerSteps.Count - 1;
        //stepManager.stepSlider.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "EndValueText");
    }
}
