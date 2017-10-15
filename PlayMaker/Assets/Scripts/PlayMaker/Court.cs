using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Court : OnCourtObject {

    public CourtData courtData = new CourtData();
    public static char[] validSaveCourtCharacters = "1234567890&#/|@:-.,".ToCharArray();
    public RawImage courtImage;
    bool preserveCourtAspectRatio = true;
    bool enforceScreenBoundary = true;
    

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddPlayer(int stepId, Player player)
    {
        courtData.playerSteps[stepId].Add(player);
        courtData.nextPlayerId++;
    }

    public void SetCourtDimensions(float courtScreenWidth, float courtScreenHeight, ScreenManager sm)
    {
        float scaleX, scaleY;
        RectTransform rectTransform = this.GetComponent<RectTransform>();
        if (enforceScreenBoundary)
        {
            courtScreenWidth = Math.Min(courtScreenWidth, Screen.width);
            courtScreenHeight = Math.Min(courtScreenHeight, Screen.height);
        }
        if (preserveCourtAspectRatio)
        {
            scaleX = Math.Min(courtScreenWidth / Game.courtWidth, courtScreenHeight / Game.courtHeight);
            scaleY = scaleX;
        }
        else
        {
            scaleX = courtScreenWidth / Game.courtWidth;
            scaleY = courtScreenHeight / Game.courtHeight;
        }

        courtScreenWidth = scaleX * Game.courtWidth;
        courtScreenHeight = scaleY * Game.courtHeight;

        courtImage.rectTransform.localScale = new Vector3(scaleY, scaleX, 1);
        rectTransform.position = new Vector3(courtScreenWidth/2, courtScreenHeight/2, 0);

        sm.courtScreenWidth = (int)Math.Round(courtScreenWidth);
        sm.courtScreenHeight = (int)Math.Round(courtScreenHeight);
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
}
