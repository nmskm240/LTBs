using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CSVReader 
{
    public static List<string[]> Read(string textAsset)
    {
        List<string[]> readData = new List<string[]>();
        StringReader stringReader = new StringReader(textAsset);

        while(stringReader.Peek() != -1)
        {
            string line = stringReader.ReadLine();
            readData.Add(line.Split(','));
        }
        return readData;
    }
}