using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public UIManager uiManager;
    public Court court;
    public string pathToPlaySaveFile;

    public List<string> inspectorPrefabNames;
    public List<GameObject> inspectorPrefabObjects;
    public Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < inspectorPrefabObjects.Count; i++)
        {
            prefabs.Add(inspectorPrefabNames[i], inspectorPrefabObjects[i]);
        }

        string savedPlayString = "";

        foreach (string line in MyFileReader.Read(pathToPlaySaveFile))
        {
            Debug.Log(line);
            if (FindFirstNotAny(line, Court.validSaveCourtCharacters) != null)
            {
                throw new System.Exception("Invalid characters in save file!");
            }

            savedPlayString = line;
        }

        court.courtData = CourtData.FromString(savedPlayString, court, this);
    }

	// Update is called once per frame
	void Update () {
		
	}


    public static string FindFirstNotAny(string value, params char[] charset)
    {
        string firstInvalid = value.TrimStart(charset);
        return firstInvalid.Length > 0 ? firstInvalid : null;
    }
}
