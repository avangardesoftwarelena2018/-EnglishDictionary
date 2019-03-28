using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class EnglishDictionaryManager
{
    private static EnglishDictionary englishDictionary;

    private static string projectFilePath = "/englishDictionary.json";

    public static EnglishDictionary EnglishDictionary
    {
        get => LoadData();
    }
    //Update and Save modified Data in files
    public static void UpdateDictionary(Dictionary<string, string> words)
    {
        englishDictionary.words = new List<Word>();
        foreach (var item in words)
        {
            englishDictionary.words.Add(new Word
            {
                word = item.Key,
                definition = item.Value
            });
        }
        SaveData(englishDictionary);
    }

    //LoadData from json file
    private static EnglishDictionary LoadData()
    {
        string filePath = Application.dataPath + projectFilePath;
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            englishDictionary = JsonUtility.FromJson<EnglishDictionary>(dataAsJson);
        }
        else
        {
            englishDictionary = new EnglishDictionary();
        }
        return englishDictionary;
    }

    //SaveData in json file
    private static void SaveData(EnglishDictionary englishDictionary)
    {
        string filePath = Application.dataPath + projectFilePath;
        string dataAsJson = JsonUtility.ToJson(englishDictionary);
        File.WriteAllText(filePath, dataAsJson);
    }

}
