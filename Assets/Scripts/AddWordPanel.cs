using UnityEngine;
using UnityEngine.UI;

public class AddWordPanel : MonoBehaviour
{
    [SerializeField]
    private UIDataManager UIDataManager = null;
    [SerializeField]
    private InputField wordInputField = null;
    [SerializeField]
    private InputField definitionInputField = null;

    //Save added Word on buttonPress
    public void Save()
    {
        wordInputField.interactable = true;
        string word = wordInputField.text;
        string definition = definitionInputField.text;
        if (!UIDataManager.wordsDict.ContainsKey(word))
        {
            UIDataManager.wordsDict.Add(word, definition);
            EnglishDictionaryManager.UpdateDictionary(UIDataManager.wordsDict);
        }
        else
        {
            print("this word exist");
        }
        wordInputField.text = "";
        definitionInputField.text = "";
    }

    //Clear and reset UI fields on buttonPress
    public void Cancel()
    {
        wordInputField.interactable = true;
        wordInputField.text = "";
        definitionInputField.text = "";
    }
}
