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
    public virtual Vector3 GetLookAtPosition { get { return end; } }
    public Vector3 start, end, lookAtPosition;
    public bool isStarted = false;
    public bool isDone = false;

    public void BuildPlayerAction(Vector3 startPoint, Vector3 endPoint, PlayerStep dragActionPlayer)
    {
        Vector3 components = endPoint - startPoint;
        start = startPoint;
        end = endPoint;

        float angleInRad = -Mathf.Atan(components.z / components.x);
        float angleInDeg = components.x < 0 ? angleInRad * Mathf.Rad2Deg - 180 : angleInRad * Mathf.Rad2Deg;
        transform.Rotate(new Vector3(0, angleInDeg, 0));
        // 1.5f magic number because of whitespace in step images?
        transform.localScale = new Vector3(components.magnitude * 1.5f, transform.localScale.y, transform.localScale.z);
        transform.SetParent(dragActionPlayer.transform);
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
        Vector3 pos = player.transform.position;
        pos.y = 0;

        Ray ray = new Ray(startingPoint, direction);
        return Vector3.Cross(ray.direction, pos - ray.origin).magnitude;
    }
}
