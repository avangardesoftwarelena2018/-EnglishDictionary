using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditWordPanel : MonoBehaviour
{
    [SerializeField]
    private UIDataManager UIDataManager = null;
    [SerializeField]
    private Text wordText = null;
    [SerializeField]
    private InputField definitionInputField = null;

    public void Save()
    {
        string word = wordText.text;
        string definition = definitionInputField.text;
        UIDataManager.wordsDict[word] = definition;
        EnglishDictionaryManager.UpdateWord(word, definition);
    }
}
