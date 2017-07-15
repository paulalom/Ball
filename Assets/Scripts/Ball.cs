using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    
    public Person person;
    public Net currentNet;

    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(250f/255f,131f/255f,32f/255f);
    }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TopNetCollider")
        {
            currentNet = other.gameObject.GetComponentInParent<Net>();
        }
        else if(other.gameObject.name == "BottomNetCollider" && currentNet == other.gameObject.GetComponentInParent<Net>())
        {
            currentNet.Score();
            currentNet = null;
        }
    }
}
