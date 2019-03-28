using System;
using System.Collections.Generic;

[Serializable]
public class EnglishDictionary
{
    public List<Word> words = new List<Word>();
}

[Serializable]
public class Word
{
    public string word;
    public string definition;
}

