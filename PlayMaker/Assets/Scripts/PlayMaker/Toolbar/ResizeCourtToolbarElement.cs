using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResizeCourtToolbarElement : ToolbarElement {

    public InputField courtWidthInputText;
    public InputField courtHeightInputText;
    
    void Start()
    {
        screenManager.onCourtResize.AddListener(OnCourtResize);
    }

    void OnCourtResize()
    {
        courtWidthInputText.text = screenManager.courtScreenWidth.ToString();
        courtHeightInputText.text = screenManager.courtScreenHeight.ToString();
    }
	
    public void TrySetCourtWidth()
    {
        float width;
        if (float.TryParse(courtWidthInputText.text, out width))
        {
            screenManager.courtScreenWidth = (int)width;
        }
        else
        {
            courtWidthInputText.text = screenManager.courtScreenWidth.ToString();
        }
    }

    public void TrySetCourtHeight()
    {
        float height;
        if (float.TryParse(courtHeightInputText.text, out height))
        {
            screenManager.courtScreenHeight = (int)height;
        }
        else
        {
            courtHeightInputText.text = screenManager.courtScreenHeight.ToString();
        }
    }

}
