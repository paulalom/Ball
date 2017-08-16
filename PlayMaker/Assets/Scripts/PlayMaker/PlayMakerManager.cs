using UnityEngine;
using System.Collections.Generic;

public class PlayMakerManager : MonoBehaviour {
    
    public PlayMakerScreenManager screenManager;
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
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
