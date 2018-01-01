using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStep : OnCourtObject {

    public int playerId;
    public List<PlayerStepAction> playerActions = new List<PlayerStepAction>();
    public SpriteRenderer spriteRenderer;
    public TextMesh text;

    void Start()
    {
    }
}
