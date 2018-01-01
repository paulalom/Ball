using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPlayerManager : MonoBehaviour {

    private List<Player> virtualPlayers = new List<Player>();
    public Court court;
    public StepManager stepManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        
		foreach (Player player in virtualPlayers)
        {
            PlayerStepAction step = court.courtData.playerSteps[stepManager.currentStep][player.playerId].playerActions.FirstOrDefault();
            Vector3 currentPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            Vector3 destination = new Vector3(step.end.x, player.transform.position.y, step.end.z);
            player.transform.position = Vector3.MoveTowards(currentPos, destination, 3f * Time.deltaTime);

            Animation anim = player.GetComponentInChildren<Animation>();
            if (step.IsStarted(player) && !anim.isPlaying)
            {
                player.transform.LookAt(step.GetLookAtPosition);
                anim.Play();
            }
            else if (step.IsDone(player) && anim.isPlaying)
            {
                anim.Stop();
                player.transform.LookAt(step.GetLookAtPosition);
            }
        }
	}

    public void SpawnVirtualPlayers(Dictionary<string, GameObject> prefabs)
    {
        foreach (PlayerStep playerStep in court.courtData.playerSteps[0])
        {
            if (playerStep.playerId != court.courtData.humanPlayerId)
            {
                GameObject virtualPlayer = Instantiate(prefabs["Player"]);
                virtualPlayer.transform.position = new Vector3(playerStep.transform.position.x, 0, playerStep.transform.position.z);
                Player player = virtualPlayer.GetComponent<Player>();
                virtualPlayers.Add(player);
                player.playerId = virtualPlayers.Count;
                player.modelPrefab = prefabs["PlayerModel" + (virtualPlayers.Count < 4 ? virtualPlayers.Count : 3)];
            }
        }
    }
}
