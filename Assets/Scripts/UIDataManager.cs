using System.Collections;
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
        foreach (var item in EnglishDictionaryManager.EnglishDictionary.words)
        {
            if (!wordsDict.ContainsKey(item.word))
            {
                wordsDict.Add(item.word, item.definition);
            }
        }
        inputField.onValueChanged.AddListener(ShowAutocomplete);
    }
    
    public void SearchButton()
    {
        searchedWord = inputField.text;
        FindWord(searchedWord);
    }

    public void AddWordButton()
    {
        addWordDefinitionPanel.SetActive(true);
    }

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

    private void FindWord(string word)
    {
        ClearContent();
        if (wordsDict.ContainsKey(word))
        {
            if (wordsDict.TryGetValue(word, out string value))
            {
                GameObject wordItem = Instantiate(prefab, content);
                wordItem.GetComponent<WordItem>().Intantiate(word, value, ShowWord, EditWord, DeleteWord);
                gameObjectList.Add(wordItem);
            }
        }
        else
        {
            addWordDefinitionPanel.SetActive(true);
            addWordDefinitionPanel.transform.GetChild(0).GetComponentInChildren<InputField>().text = word;
            addWordDefinitionPanel.transform.GetChild(0).GetComponentInChildren<InputField>().interactable = false;
        }
    }

    private void ShowAutocomplete(string inputText)
    {
        ClearContent();
        string inputStartsWith = inputText.Length >= 2
                                ? inputText.Substring(0, inputText.Length)
                                : null;

        if (!string.IsNullOrEmpty(inputText) && !string.IsNullOrEmpty(inputStartsWith))
        {
            var dict = wordsDict.Where(w => w.Key.StartsWith(inputStartsWith));
            foreach (var item in dict)
            {
                if (wordsDict.TryGetValue(item.Key, out string value))
                {
                    GameObject wordItem = Instantiate(prefab, content);
                    wordItem.GetComponent<WordItem>().Intantiate(item.Key, value, ShowWord, EditWord, DeleteWord);
                    gameObjectList.Add(wordItem);
                }
            }
        }
    }
    
    private void ClearContent()
    {
        sortedWordsText.SetActive(false);
        foreach (var item in gameObjectList)
        {
            Destroy(item);
        }
    }

    private void ShowWord(string word)
    {
        showWordDefinitionPanel.SetActive(true);
        if (wordsDict.TryGetValue(word, out string value))
        {
            showWordDefinitionPanel.transform.GetChild(0).GetComponentInChildren<Text>().text = word;
            showWordDefinitionPanel.transform.GetChild(1).GetComponentInChildren<Text>().text = value;
        }
    }

    private void EditWord(string word)
    {
        editWordDefinitionPanel.SetActive(true);
        if (wordsDict.TryGetValue(word, out string value))
        {
            editWordDefinitionPanel.transform.GetChild(0).GetComponentInChildren<Text>().text = word;
            editWordDefinitionPanel.transform.GetChild(1).GetComponentInChildren<InputField>().text = value;
        }
    }

    private void DeleteWord(string word)
    {
        wordsDict.Remove(word);
        EnglishDictionaryManager.UpdateDictionary(wordsDict);
        foreach (var item in gameObjectList)
        {
            if (item.GetComponentInChildren<Text>().text == word)
            {
                Destroy(item);
            }
        }
    }
}
