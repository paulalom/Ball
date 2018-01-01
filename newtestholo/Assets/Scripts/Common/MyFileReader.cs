using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class MyFileReader {

    public static List<string> Read(string path)
    {
        try
        {
            List<string> lines = new List<string>();
            Stream stream = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(stream))
            {
                return new List<string>(reader.ReadToEnd().Split('\n'));
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception reading file: " + e.Message);
            return new List<string>();
        }
    }
}
