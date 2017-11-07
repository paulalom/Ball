using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    
    public Player person;
    Net currentNet;

    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(250f/255f,131f/255f,32f/255f);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TopGoalCollider" && GetComponent<Rigidbody>().velocity.y < 0)
        {
            currentNet = other.GetComponentInParent<Net>();
        }
        if (other.gameObject.name == "BotGoalCollider" && GetComponent<Rigidbody>().velocity.y < 0 && currentNet == other.GetComponentInParent<Net>())
        {
            other.GetComponentInParent<Net>().Score();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "GoalColliderWrapper" && GetComponent<Rigidbody>().velocity.y < 0)
        {
            currentNet = null;
        }
    }
}
