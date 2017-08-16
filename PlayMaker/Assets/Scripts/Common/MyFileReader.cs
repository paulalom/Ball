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
            string line;
            StreamReader reader = new StreamReader(path, Encoding.Default);
            using (reader)
            {
                line = reader.ReadLine();

                while (line != null)
                {
                    lines.Add(line);
                    line = reader.ReadLine();
                }
                reader.Close();
                return lines;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception reading file: " + e.Message);
            return new List<string>();
        }
    }
}
