using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : OnCourtObject
{
    public int playerTypeId;
    public List<PlayerAction> playerActions = new List<PlayerAction>();
    public Image image;
    public Text text;

    void Start()
    {
    }
}
