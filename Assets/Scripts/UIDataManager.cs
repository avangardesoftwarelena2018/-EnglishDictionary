using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDataManager : MonoBehaviour
{
    [SerializeField]
    private InputField inputField = null;
    [SerializeField]
    private Transform content = null;
    [SerializeField]
    private GameObject prefab = null;
    [SerializeField]
    private GameObject wordPanelDefinition = null;
    private List<GameObject> gameObjectList = new List<GameObject>();
    public Dictionary<string, string> wordsDict = new Dictionary<string, string>();
    private string searchedWord = "";

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in EnglishDictionaryManager.EnglishDictionary.words)
        {
            if (!wordsDict.ContainsKey(item.word))
            {
                wordsDict.Add(item.word, item.definition);
            }
        }

        inputField.onEndEdit.AddListener(delegate { Search(inputField);});
        UpdateContent();
    }

    public void SearchButton()
    {
        searchedWord = inputField.text;
        FindWord(searchedWord);
    }

    public void AddButton()
    {
        string word = "word";
        string definition = "definition";
        if (!wordsDict.ContainsKey(word))
        {
            wordsDict.Add(word, definition);
            EnglishDictionaryManager.AddWord(word, definition);
        }
        else
        {
            print("this word exist");
        }
        UpdateContent();
    }

    private void Search(InputField inputField)
    {
        searchedWord = inputField.text;
        FindWord(searchedWord);
        print(inputField.text);
    }

    private void FindWord(string word)
    {
        if (wordsDict.ContainsKey(word))
        {
            foreach (var item in gameObjectList)
            {
                Destroy(item);
            }

            GameObject uiWord = Instantiate(prefab, content);
            gameObjectList.Add(uiWord);
        }
    }
    private void UpdateContent()
    {
        foreach (var item in gameObjectList)
        {
            Destroy(item);
        }
        foreach (var item in wordsDict)
        {
            GameObject wordItem = Instantiate(prefab, content);
            wordItem.GetComponent<WordItem>().Intantiate(item.Key, item.Value, ShowWord, EditWord, DeleteWord);
            gameObjectList.Add(wordItem);
        }
    }

    private void ShowWord(string word)
    {
        wordPanelDefinition.SetActive(true);
        if (wordsDict.TryGetValue(word, out string value))
        {
            wordPanelDefinition.GetComponentInChildren<Text>().text = value;
        }
    }

    private void EditWord(string word)
    {
        print("EditWord");
    }

    private void DeleteWord(string word)
    {
        print("DeleteWord");
    }
}
