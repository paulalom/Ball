using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : OnCourtObject {

    public int playerId;
    public List<PlayerAction> playerActions;
    public SpriteRenderer spriteRenderer;
    public TextMesh text;

    void Start()
    {
        playerActions = new List<PlayerAction>();
    }
}
