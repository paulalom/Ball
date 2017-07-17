using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image throwPowerBar;
    public Person person;

    void OnGUI()
    {
        throwPowerBar.fillAmount = person.throwForce / person.maxThrowForce;
    }
}
