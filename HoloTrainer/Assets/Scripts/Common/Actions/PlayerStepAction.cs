using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActionType
{
    Move,
    Dribble,
    Pass,
    Pick,
    Shoot
}
public abstract class PlayerStepAction : OnCourtObject
{
    public const float minCompletionDistance = 2f;
    public const float maxDistanceAllowedFromPath = 3f;
    public PlayerActionType stepType;
    public SpriteRenderer spriteRenderer;
    public Vector3 start, end;
    public bool isStarted = false;

    public void BuildPlayerAction(Vector3 startPoint, Vector3 endPoint, PlayerStep dragActionPlayer)
    {
        Vector3 components = endPoint - startPoint;
        start = startPoint;
        end = endPoint;
        
        float angleInRad = -Mathf.Atan(components.z / components.x);
        float angleInDeg = components.x < 0 ? angleInRad * Mathf.Rad2Deg - 180 : angleInRad * Mathf.Rad2Deg;
        transform.Rotate(new Vector3(0, angleInDeg, 0));
        transform.SetParent(dragActionPlayer.transform);
        // VERY BAD MAGIC 1.5 HERE... WHY!!?!?!??!?
        transform.localScale = new Vector3(components.magnitude/1.47f, transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(components.x / 2, 0f, components.z / 2);
        
        Debug.Log(components.magnitude);
    }

    public abstract bool IsStarted(Player player);
    public abstract bool IsDone(Player player);
    public abstract bool ValidateAction(Player player);

    protected float GetDistanceToStepLine(Player player)
    {
        Vector3 direction = (end - start).normalized;
        Vector3 startingPoint = start;

        Ray ray = new Ray(startingPoint, direction);
        return Vector3.Cross(ray.direction, player.transform.position - ray.origin).magnitude;
    }
}
