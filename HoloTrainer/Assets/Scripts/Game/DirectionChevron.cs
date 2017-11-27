using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChevron : MonoBehaviour {

    public Vector3 pointTo;
    public new Camera camera;

	void Update () {
        Vector3 screenPos = camera.WorldToScreenPoint(pointTo);
        Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

        if (screenPos.z > 0 
            && screenPos.x > 0 && screenPos.x < Screen.width
            && screenPos.y > 0 && screenPos.y < Screen.height)
        {
            screenPos -= screenCenter;
            transform.localPosition = Vector3.zero;
            transform.parent.rotation = Quaternion.Euler(0, 0, -90);
            transform.parent.localPosition = screenPos + new Vector3(0, Screen.height * .1f, 0);
        }
        else
        {
            transform.parent.localPosition = Vector3.zero;
            // invert position of object when it's behind the screen
            if (screenPos.z < 0)
            {
                screenPos *= -1;
            }
            
            // move origin to center of screen
            screenPos -= screenCenter;

            float angle = Mathf.Atan2(screenPos.y, screenPos.x);

            transform.localPosition = new Vector3(Mathf.Min((float)Screen.width, (float)Screen.height) * 0.45f, 0, 0);
            transform.parent.rotation = Quaternion.Euler(0,0, Mathf.Rad2Deg * angle);
            /*
            float cos = Mathf.Cos(angle);
            float sin = -Mathf.Sin(angle);

            screenPos = screenCenter + new Vector3(sin * 150, cos * 150, 0);
            // y=mx+b
            float m = cos / sin;

            Vector3 screenBounds = screenCenter * 0.9f;

            if (cos > 0)
            {

            }
            else
            {

            }*/
        }
	}
}
