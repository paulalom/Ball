using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : OnCourtObject
{
    public int playerId;
    public List<PlayStep> playSteps;
    public Image image;
    public Text text;

    void Start()
    {
        playSteps = new List<PlayStep>();
    }
}
