using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveLoadToolbarElement : ToolbarElement {

    string defaultFilePath = "court.txt";
    string loadSaveFilePath;
    public Court court;
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

    public void LoadCourt(PlayMakerManager playMakerManager)
    {
        foreach (string line in MyFileReader.Read(loadSaveFilePath))
        {
            Debug.Log(line);
            if (FindFirstNotAny(line, Court.validSaveCourtCharacters) != null)
            {
                throw new System.Exception("Invalid characters in save file!");
            }
            
            playMakerManager.LoadCourt(line);
        }
    }

    public void SaveCourt()
    {
        MyFileWriter.Write(loadSaveFilePath, court.ToString());
    }

    public static string FindFirstNotAny(string value, params char[] charset)
    {
        string firstInvalid = value.TrimStart(charset);
        return firstInvalid.Length > 0 ? firstInvalid : null;
    }
}
