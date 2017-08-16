using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveLoadToolbarElement : ToolbarElement {

    string defaultFilePath = "court.txt";
    string loadSaveFilePath;
    public PlayMakerCourt court;
    public InputField fileNameInputField;
    public Text fileNameLabel;

	// Use this for initialization
	void Start () {
        loadSaveFilePath = defaultFilePath;
        fileNameLabel.text = defaultFilePath;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetFilePath()
    {
        if (fileNameInputField.text != "")
        {
            loadSaveFilePath = fileNameInputField.text + ".txt";
        }
        else
        {
            loadSaveFilePath = defaultFilePath;
        }
        fileNameLabel.text = "FileName: " + loadSaveFilePath;
    }

    public void LoadCourt()
    {
        foreach (string line in MyFileReader.Read(loadSaveFilePath))
        {
            Debug.Log(line);
        }
    }

    public void SaveCourt()
    {
        MyFileWriter.Write(loadSaveFilePath, court.ToString());
    }
}
