using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolbarElement : MonoBehaviour {

    public PlayMakerScreenManager screenManager;
    public RectTransform thisRectTransform;
    public RectTransform dragToolbarButtonTransform;
    public int minWidth;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void DragToolbarElement()
    {
        thisRectTransform.position = Input.mousePosition +
            new Vector3(thisRectTransform.rect.width + dragToolbarButtonTransform.rect.x - dragToolbarButtonTransform.rect.width/2,
                        -thisRectTransform.rect.height - dragToolbarButtonTransform.rect.y + dragToolbarButtonTransform.rect.height/2, 0);
    }
}
