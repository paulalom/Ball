using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtData {

    public List<List<Player>> playerSteps = new List<List<Player>>();
    
    /// <summary>
    /// Need to convert 2d x,y worldspace to 3d worldspace x,y,z
    /// Mapping is x,y->y,0,x
    /// </summary>
    /// <param name="courtString"></param>
    /// <param name="court"></param>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public static CourtData FromString(string courtString, Court court, GameManager gameManager)
    {
        CourtData data = new CourtData();
        PlayerAdder playerAdder = new PlayerAdder();
        PlayerActionAdder playerActionAdder = new PlayerActionAdder();
        playerActionAdder.uiManager = gameManager.uiManager;
        playerAdder.uiManager = gameManager.uiManager;

        float courtSizeXRatio = 1f, courtSizeZRatio = 1f, courtSizeX, courtSizeZ;
        string[] dataStrings = courtString.Split('#');
        string[] savedCourtSize = dataStrings[0].Split(',');

        float courtOffsetX, courtOffsetZ;

        // Court size ratio is z/2dXval, 0, x/2dYval
        courtSizeX = float.Parse(savedCourtSize[1]);
        courtSizeZ = float.Parse(savedCourtSize[0]);

        courtSizeXRatio = gameManager.court.transform.localScale.x / courtSizeX;
        courtSizeZRatio = -gameManager.court.transform.localScale.z / courtSizeZ;

        courtOffsetX = 0;// courtSizeX / 2 * courtSizeXRatio;
        courtOffsetZ = 0; // courtSizeZ / 2 * courtSizeZRatio;

        string[] steps = dataStrings[1].Split('&');
        foreach (string step in steps) {

            string[] players = step.Split('|');
            List<Player> stepPlayers = new List<Player>();

            foreach (string playerData in players)
            {
                string[] temp = playerData.Split(':');
                string playerString = temp[0];
                string playerPlaySteps = temp[1];

                temp = playerString.Split('@');
                string playerId = temp[0];
                playerAdder.playerId = int.Parse(playerId);

                string[] playerPosStrings = temp[1].Split(',');
                Vector3 playerPos = new Vector3(courtOffsetX + float.Parse(playerPosStrings[1]) * courtSizeXRatio,
                                                 .55f,
                                                 courtOffsetZ + float.Parse(playerPosStrings[0]) * courtSizeZRatio);

                Player player = (Player)playerAdder.GetOnCourtObject(gameManager.prefabs);
                player.transform.SetParent(court.transform);
                player.transform.position = playerPos;
                stepPlayers.Add(player);

                if (playerPlaySteps == "")
                {
                    continue;
                }

                foreach (string playStepData in playerPlaySteps.Split('/'))
                {
                    string[] temp2 = playStepData.Split('@');
                    string stepType = temp2[0];
                    string[] actionPositions = temp2[1].Split('-');

                    playerActionAdder.playStepId = (PlayerActionType)int.Parse(stepType);

                    string[] stepStart = actionPositions[0].Split(',');
                    string[] stepEnd = actionPositions[1].Split(',');

                    PlayerAction playerAction = (PlayerAction)playerActionAdder.GetOnCourtObject(gameManager.prefabs);
                    Vector3 startPoint = new Vector3(courtOffsetX + float.Parse(stepStart[1]) * courtSizeXRatio,
                                                 0f,
                                                 courtOffsetZ + float.Parse(stepStart[0]) * courtSizeZRatio);
                    Vector3 endPoint = new Vector3(courtOffsetX + float.Parse(stepEnd[1]) * courtSizeXRatio,
                                                 0f,
                                                 courtOffsetZ + float.Parse(stepEnd[0]) * courtSizeZRatio);

                    Debug.Log(courtSizeXRatio + "... " + courtSizeZRatio);
                    playerAction.BuildPlayerAction(startPoint, endPoint, player);
                    player.playerActions.Add(playerAction);
                }
            }
            data.playerSteps.Add(stepPlayers);
        }
        return data;
    }
}
