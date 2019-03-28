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

    //Save edited Word on buttonPress
    public void Save()
    {
        string word = wordText.text;
        string definition = definitionInputField.text;
        UIDataManager.wordsDict[word] = definition;
        EnglishDictionaryManager.UpdateDictionary(UIDataManager.wordsDict);
    }
}
