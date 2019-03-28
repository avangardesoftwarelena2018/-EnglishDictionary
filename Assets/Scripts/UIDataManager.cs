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
            CreateWordItem(word);
        }
        else if (!string.IsNullOrEmpty(word))
        {
            addWordDefinitionPanel.SetActive(true);
            addWordDefinitionPanel.GetComponent<AddWordPanel>().Word = word;
            addWordDefinitionPanel.GetComponent<AddWordPanel>().SetWordInputFieldInteractable(false);
        }
    }

    //Instantiate word item and intitialize with values
    private void CreateWordItem(string word)
    {
        if (wordsDict.TryGetValue(word, out string value))
        {
            GameObject wordItem = Instantiate(prefab, content);
            wordItem.GetComponent<WordItem>().Initialize(word, value, ShowWord, EditWord, DeleteWord);
            gameObjectList.Add(wordItem);
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
                CreateWordItem(item.Key);
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
        if (wordsDict.TryGetValue(word, out string definition))
        {
            showWordDefinitionPanel.GetComponent<ShowWordPanel>().Word = word;
            showWordDefinitionPanel.GetComponent<ShowWordPanel>().Definition = definition;
        }
    }

    //Edit Word on button press opens panel for editing word and definition
    private void EditWord(string word)
    {
        editWordDefinitionPanel.SetActive(true);
        if (wordsDict.TryGetValue(word, out string definition))
        {
            editWordDefinitionPanel.GetComponent<EditWordPanel>().Word = word;
            editWordDefinitionPanel.GetComponent<EditWordPanel>().Definition = definition;
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
