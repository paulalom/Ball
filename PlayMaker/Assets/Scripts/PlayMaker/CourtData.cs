using UnityEngine;
using System.Collections.Generic;

public class CourtData {

    public List<List<Player>> playerSteps = new List<List<Player>>() { new List<Player>() };
    public PlayMakerManager playMakerManager;
    public int nextPlayerId = 0;
    public int humanPlayerId = 0;

    public CourtData()
    {
    }

    public CourtData(List<List<Player>> playerSteps)
    {
        this.playerSteps = playerSteps;
    }

    public override string ToString()
    {
        string width = playMakerManager.screenManager.courtScreenWidth.ToString();
        string height = playMakerManager.screenManager.courtScreenHeight.ToString();
        string courtString = humanPlayerId + "#" + width + "," + height + "#";

        // subtract half screen to move to origin
        foreach (List<Player> step in playerSteps)
        {
            foreach (Player player in step)
            {
                // Player
                RectTransform playerRect = player.GetComponent<RectTransform>();
                courtString += player.playerTypeId + "@" 
                            + (playerRect.position.x - Screen.width/2)
                            + "," 
                            + (playerRect.position.y - Screen.height/2) + ":";

                // Player Action
                foreach (PlayerAction playerAction in player.playerActions)
                {
                    courtString += (int)playerAction.actionType + "@" 
                        + (playerAction.start.x - Screen.width / 2) + "," 
                        + (playerAction.start.y - Screen.height / 2) + "," 
                        + playerAction.start.z + "="
                        + (playerAction.end.x - Screen.width / 2) + "," 
                        + (playerAction.end.y - Screen.height / 2) + "," 
                        + playerAction.end.z + "/";
                }
                courtString = courtString.TrimEnd('/');
                courtString += "|";
            }
            courtString = courtString.TrimEnd('|');
            courtString += "&";
        }
        courtString = courtString.TrimEnd('&');
        return courtString;
    }

    public static CourtData FromString(string courtString, Court court, PlayMakerManager playMakerManager)
    {
        CourtData data = new CourtData(new List<List<Player>>());
        PlayerBuilder playerAdder = new PlayerBuilder(0);
        PlayerActionBuilder playStepAdder = new PlayerActionBuilder(0);
        data.playMakerManager = playMakerManager;

        float courtSizeXRatio = 1f, courtSizeYRatio = 1f, courtSizeX, courtSizeY;
        string[] dataStrings = courtString.Split('#');
        string humanPlayerId = dataStrings[0];
        string[] savedCourtSize = dataStrings[1].Split(',');

        courtSizeX = float.Parse(savedCourtSize[0]);
        courtSizeY = float.Parse(savedCourtSize[1]);
        
        playMakerManager.screenManager.ResizeCourt();

        courtSizeXRatio = (float)playMakerManager.screenManager.courtScreenWidth/(int)courtSizeX;
        courtSizeYRatio = (float)playMakerManager.screenManager.courtScreenHeight/(int)courtSizeY;

        string[] steps = dataStrings[2].Split('&');
        foreach (string step in steps)
        {
            List<Player> stepPlayers = new List<Player>();
            string[] players = step.Split('|');

            foreach (string playerData in players)
            {
                string[] temp = playerData.Split(':');
                string playerString = temp[0];
                string playerPlaySteps = temp[1];

                temp = playerString.Split('@');
                string playerId = temp[0];
                playerAdder.playerTypeId = int.Parse(playerId);

                string[] playerPosStrings = temp[1].Split(',');
                Vector3 playerPos = new Vector3(float.Parse(playerPosStrings[0]) * courtSizeXRatio + Screen.width/2,
                                                 float.Parse(playerPosStrings[1]) * courtSizeYRatio + Screen.height/2,
                                                 0);

                Player player = (Player)playerAdder.GetOnCourtObject(playMakerManager.prefabs);
                player.transform.SetParent(court.transform, false);
                player.GetComponent<RectTransform>().position = playerPos;
                stepPlayers.Add(player);

                if (playerPlaySteps == "")
                {
                    continue;
                }

                foreach (string playStepData in playerPlaySteps.Split('/'))
                {
                    string[] temp2 = playStepData.Split('@');
                    string stepType = temp2[0];
                    string[] actionPositions = temp2[1].Split('=');

                    playStepAdder.playerActionId = (PlayerActionType)int.Parse(stepType);

                    string[] stepStart = actionPositions[0].Split(',');
                    string[] stepEnd = actionPositions[1].Split(',');

                    PlayerAction playerAction = (PlayerAction)playStepAdder.GetOnCourtObject(playMakerManager.prefabs);
                    Vector3 startPoint = new Vector3(float.Parse(stepStart[0]) * courtSizeXRatio + Screen.width / 2,
                                                 float.Parse(stepStart[1]) * courtSizeYRatio + Screen.height / 2,
                                                 float.Parse(stepStart[2]));
                    Vector3 endPoint = new Vector3(float.Parse(stepEnd[0]) * courtSizeXRatio + Screen.width / 2,
                                                 float.Parse(stepEnd[1]) * courtSizeYRatio + Screen.height / 2,
                                                 float.Parse(stepEnd[2]));

                    playerAction.InitPlayerAction(startPoint, endPoint, player);
                    player.playerActions.Add(playerAction);
                }
            }
            data.playerSteps.Add(stepPlayers);
        }
        return data;
    }
}
