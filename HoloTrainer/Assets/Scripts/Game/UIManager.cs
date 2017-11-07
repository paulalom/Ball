using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    
    public Image throwPowerBar;
    public Player person;
    public List<Sprite> onCourtSprites;
    public DirectionChevron directionChevron;

    void OnGUI()
    {
        throwPowerBar.fillAmount = person.throwForce / person.maxThrowForce;
    }
}
