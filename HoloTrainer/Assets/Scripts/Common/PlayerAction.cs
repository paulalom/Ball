using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActionType
{
    Move,
    Dribble,
    Pass,
    Screen,
    Shoot
}
public class PlayerAction : OnCourtObject
{
    public PlayerActionType stepType;
    public SpriteRenderer spriteRenderer;
    public Vector3 start, end;

    public void BuildPlayerAction(Vector3 startPoint, Vector3 endPoint, Player dragActionPlayer)
    {
        Vector3 components = endPoint - startPoint;
        
        float angleInRad = -Mathf.Atan(components.z / components.x);
        float angleInDeg = components.x < 0 ? angleInRad * Mathf.Rad2Deg - 180 : angleInRad * Mathf.Rad2Deg;
        transform.Rotate(new Vector3(0, angleInDeg, 0));
        transform.SetParent(dragActionPlayer.transform);
        // VERY BAD MAGIC 1.5 HERE... WHY!!?!?!??!?
        transform.localScale = new Vector3(components.magnitude/1.47f, transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(components.x / 2, 0f, components.z / 2);
        
        Debug.Log(components.magnitude);
    }
}
