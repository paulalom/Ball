using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayStepType
{
    Move,
    Dribble,
    Pass,
    Screen,
    Shoot
}
public class PlayStep : OnCourtObject
{
    public int stepId;
    public Image image;
}
