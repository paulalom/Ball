using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : PlayerStepAction {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool IsStarted(Player player)
    {
        if (isStarted)
        {
            return true;
        }
        else
        {
            isStarted = Vector3.Distance(player.transform.position, start) < minCompletionDistance;
            if (isStarted)
            {
                Debug.Log("move started!");
            }
        }

        return isStarted;
    }

    public override bool IsDone(Player player)
    {
        if (isDone)
        {
            return true;
        }
        else if (ValidateAction(player) && Vector3.Distance(player.ball.transform.position, end) < minCompletionDistance)
        {
            isStarted = false;
            isDone = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool ValidateAction(Player player)
    {
        if (Vector3.Distance(player.transform.position, start) < maxDistanceAllowedFromPath)
        {
            return true;
        }
        else
        {
            isStarted = false;
            return false;
        }
    }
}
