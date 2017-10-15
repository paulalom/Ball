using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class MyFileWriter
{
    public static void Write(string path, string line)
    {
        Write(path, new List<string>() { line });
    }

    public static void Write(string path, List<string> lines)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                string str = string.Join(Environment.NewLine, lines.ToArray());
                sw.Write(str);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception writing to file: " + e.Message);
        }
    }
}
