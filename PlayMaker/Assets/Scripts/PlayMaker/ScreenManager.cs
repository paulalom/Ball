using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ScreenManager : MonoBehaviour {

    public Court court;
    public int screenWidth, screenHeight, courtScreenWidth, courtScreenHeight;
    public UnityEvent onCourtResize;
    
    void Start () {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        ResetCourtSize();
    }
    
	void Update () {
	    if (screenWidth != Screen.width || screenHeight != Screen.height)
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;
            ResizeCourt();
        }
	}
    
    public void ResizeCourt()
    {
        court.SetCourtDimensions(courtScreenWidth, courtScreenHeight, this);
        onCourtResize.Invoke();
    }

    public void ResetCourtSize()
    {
        courtScreenWidth = Screen.width;
        courtScreenHeight = Screen.height;
        ResizeCourt();
    }
}
