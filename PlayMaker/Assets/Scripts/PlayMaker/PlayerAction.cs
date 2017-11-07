using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public PlayerActionType actionType;
    public Image image;
    public Vector3 start, end;
    public bool isMoveAction;
    public Player owner;

    public void InitPlayerAction(Vector3 startPoint, Vector3 endPoint, Player dragActionPlayer)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 startPos = startPoint;
        Vector3 components = endPoint - startPoint;

        start = startPoint;
        end = endPoint;
        rectTransform.rotation = new Quaternion(0, 0, 0, 0);

        float angleInRad = Mathf.Atan(components.y / components.x);
        float angleInDeg = components.x < 0 ? angleInRad * Mathf.Rad2Deg - 180 : angleInRad * Mathf.Rad2Deg;
        rectTransform.Rotate(new Vector3(0, 0, angleInDeg));
        Debug.Log("deg: " + Mathf.Atan(components.y / components.x) * Mathf.Rad2Deg);
        transform.SetParent(dragActionPlayer.transform, false);
        rectTransform.sizeDelta = new Vector2(components.magnitude, rectTransform.rect.height);
        rectTransform.localPosition = new Vector3(components.x / 2, components.y / 2, 0);

        if (actionType == PlayerActionType.Shoot || actionType == PlayerActionType.Pass)
        {
            isMoveAction = false;
        }
        else
        {
            isMoveAction = true;
        }
        owner = dragActionPlayer;
    }
}
