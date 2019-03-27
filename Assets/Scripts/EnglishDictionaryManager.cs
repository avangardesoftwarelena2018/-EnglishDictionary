using System;
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

    public static void AddWord(string word, string definition)
    {
        Word newWord = new Word
        {
            word = word,
            definition = definition
        };
        englishDictionary.words.Add(newWord);
        SaveData(englishDictionary);
    }

    public static void UpdateWord(string word, string definition)
    {
        englishDictionary.words
            [
                englishDictionary.words.
                IndexOf(
                        englishDictionary.words.Find
                        (w => w.word.Equals(word))
                       )
            ].definition = definition;
        SaveData(englishDictionary);
    }

    public static void DeleteWord(string word)
    {
        englishDictionary.words.RemoveAt
            (
                englishDictionary.words.
                IndexOf(
                        englishDictionary.words.Find
                        (w => w.word.Equals(word))
                       )
            );
        SaveData(englishDictionary);
    }

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

    private static void SaveData(EnglishDictionary englishDictionary)
    {
        string filePath = Application.dataPath + projectFilePath;
        string dataAsJson = JsonUtility.ToJson(englishDictionary);
        File.WriteAllText(filePath, dataAsJson);
    }

}
