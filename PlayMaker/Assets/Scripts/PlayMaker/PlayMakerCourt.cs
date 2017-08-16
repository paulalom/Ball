using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayMakerCourt : MonoBehaviour {

    private CourtData courtData = new CourtData();
    public RawImage court;
    bool preserveCourtAspectRatio = true;
    bool enforceScreenBoundary = true;
    

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetCourtDimensions(float courtScreenWidth, float courtScreenHeight)
    {
        float scaleX, scaleY;
        if (enforceScreenBoundary)
        {
            courtScreenWidth = Math.Min(courtScreenWidth, Screen.width);
            courtScreenHeight = Math.Min(courtScreenHeight, Screen.height);
        }
        if (preserveCourtAspectRatio)
        {
            scaleX = Math.Min(courtScreenWidth  / Game.courtWidth, courtScreenHeight / Game.courtHeight);
            scaleY = scaleX;
        }
        else
        {
            scaleX = courtScreenWidth / Game.courtWidth;
            scaleY = courtScreenHeight / Game.courtHeight;
        }

        courtScreenWidth = scaleX * Game.courtWidth / 2;
        courtScreenHeight = Screen.height - (scaleY * Game.courtHeight / 2);

        court.rectTransform.localScale = new Vector3(scaleY, scaleX, 1);
        court.rectTransform.position = new Vector3(courtScreenWidth, courtScreenHeight, 0);
    }

    public void TogglePreserveCourtAspectRatio()
    {
        preserveCourtAspectRatio = !preserveCourtAspectRatio;
    }

    public void ToggleEnforceScreenBoundary()
    {
        enforceScreenBoundary = !enforceScreenBoundary;
    }

    public override string ToString()
    {
        return courtData.ToString();
    }

    public static CourtData FromString(string courtDataString, PlayMakerManager playMakerManager)
    {
        return Instantiate(playMakerManager.prefabs[typeof(PlayMakerCourt).ToString()]).GetComponent<CourtData>();
    }
}
