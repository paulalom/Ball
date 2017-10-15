using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    
    public Image throwPowerBar;
    public Person person;
    public List<Sprite> onCourtSprites;

    void OnGUI()
    {
        throwPowerBar.fillAmount = person.throwForce / person.maxThrowForce;
    }
}
