using UnityEngine;
using System.Collections;

public class Net : MonoBehaviour {

    public Renderer scoreLight;
    public const float scoreLightDuration = 5f;
    float remainingScoreLightDuration = 0f;
    public bool scored = false;

	// Use this for initialization
	void Start () {
        foreach (Renderer renderer in GetComponentsInChildren(typeof(Renderer)))
        {
            renderer.material.color = Color.gray;
        }
    }

    void Update()
    {
        if (scored)
        {
            remainingScoreLightDuration -= Time.deltaTime;
            if (remainingScoreLightDuration <= 0)
            {
                ScoreLightOff();
                scored = false;
            }
        }
    }

    public void Score()
    {
        remainingScoreLightDuration = scoreLightDuration;
        ScoreLightOn();
        scored = true;
    }

    void ScoreLightOn()
    {
        scoreLight.material.color = Color.red;
    }

    void ScoreLightOff()
    {
        scoreLight.material.color = Color.gray;
    }
}
