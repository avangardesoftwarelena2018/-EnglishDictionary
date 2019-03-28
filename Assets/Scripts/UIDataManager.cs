using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIDataManager : MonoBehaviour
{
    public Dictionary<string, string> wordsDict = new Dictionary<string, string>();
    [SerializeField]
    private InputField inputField = null;
    [SerializeField]
    private Transform content = null;
    [SerializeField]
    private GameObject prefab = null;
    [SerializeField]
    private GameObject addWordDefinitionPanel = null;
    [SerializeField]
    private GameObject showWordDefinitionPanel = null;
    [SerializeField]
    private GameObject editWordDefinitionPanel = null;
    [SerializeField]
    private GameObject sortedWordsText = null;
    private List<GameObject> gameObjectList = new List<GameObject>();
    private string searchedWord = "";
    

    void Start()
    {
        //Update dictionary with data from file
        foreach (var item in EnglishDictionaryManager.EnglishDictionary.words)
        {
            if (!wordsDict.ContainsKey(item.word))
            {
                wordsDict.Add(item.word, item.definition);
            }
        }
        inputField.onValueChanged.AddListener(ShowAutocompleteItems);
    }
    
    //Search Word on button press
    public void SearchButton()
    {
        searchedWord = inputField.text;
        FindWord(searchedWord);
    }

    //Open panel for adding new word on button press
    public void AddWordButton()
    {
        addWordDefinitionPanel.SetActive(true);
    }

    //Sort and show  words ascendent on button press
    public void ShowAZ()
    {
        ClearContent();
        string sortedDictionary = "";
        foreach (var item in wordsDict.OrderBy(key => key.Key))
        {
            sortedDictionary += "\n" + item.Key;
        }
        sortedWordsText.SetActive(true);
        sortedWordsText.GetComponent<Text>().text = sortedDictionary;
    }

    //Sort and show  words descendent on button press
    public void ShowZA()
    {
        ClearContent();
        string sortedDictionary = "";
        foreach (var item in wordsDict.OrderByDescending(key => key.Key))
        {
            sortedDictionary += "\n" + item.Key;
        }
        sortedWordsText.SetActive(true);
        sortedWordsText.GetComponent<Text>().text = sortedDictionary;
    }

    //Find word in dictionary or opens panel to add inexistent word from dictionary
    private void FindWord(string word)
    {
        ClearContent();
        if (wordsDict.ContainsKey(word))
        {
            if (wordsDict.TryGetValue(word, out string value))
            {
                GameObject wordItem = Instantiate(prefab, content);
                wordItem.GetComponent<WordItem>().Instantiate(word, value, ShowWord, EditWord, DeleteWord);
                gameObjectList.Add(wordItem);
            }
        }
        else if (!string.IsNullOrEmpty(word))
        {
            addWordDefinitionPanel.SetActive(true);
            addWordDefinitionPanel.transform.GetChild(0).GetComponentInChildren<InputField>().text = word;
            addWordDefinitionPanel.transform.GetChild(0).GetComponentInChildren<InputField>().interactable = false;
        }
    }

    //Show Autocomplete Items with words that starts with text wrote by user
    private void ShowAutocompleteItems(string inputText)
    {
        ClearContent();
        if (!string.IsNullOrEmpty(inputText))
        {
            var dict = wordsDict.Where(w => w.Key.StartsWith(inputText));
            foreach (var item in dict)
            {
                if (wordsDict.TryGetValue(item.Key, out string value))
                {
                    GameObject wordItem = Instantiate(prefab, content);
                    wordItem.GetComponent<WordItem>().Instantiate(item.Key, value, ShowWord, EditWord, DeleteWord);
                    gameObjectList.Add(wordItem);
                }
            }
        }
    }
    
    //Remove all items from UI
    private void ClearContent()
    {
        sortedWordsText.SetActive(false);
        foreach (var item in gameObjectList)
        {
            Destroy(item);
        }
    }

    //Show Word on button press opens panel for showing word and definition
    private void ShowWord(string word)
    {
        showWordDefinitionPanel.SetActive(true);
        if (wordsDict.TryGetValue(word, out string value))
        {
            showWordDefinitionPanel.transform.GetChild(0).GetComponentInChildren<Text>().text = word;
            showWordDefinitionPanel.transform.GetChild(1).GetComponentInChildren<Text>().text = value;
        }
    }

    //Edit Word on button press opens panel for editing word and definition
    private void EditWord(string word)
    {
        editWordDefinitionPanel.SetActive(true);
        if (wordsDict.TryGetValue(word, out string value))
        {
            editWordDefinitionPanel.transform.GetChild(0).GetComponentInChildren<Text>().text = word;
            editWordDefinitionPanel.transform.GetChild(1).GetComponentInChildren<InputField>().text = value;
        }
    }

    //Delete Word removes word from dictionary and updates UI without removed item
    private void DeleteWord(string word)
    {
        wordsDict.Remove(word);
        EnglishDictionaryManager.UpdateDictionary(wordsDict);
        foreach (var item in gameObjectList)
        {
            if (item != null && item.GetComponentInChildren<Text>().text == word)
            {
                Destroy(item);
            }
        }
    }
}
