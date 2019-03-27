using System.Collections;
using System.Collections.Generic;
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
    private List<GameObject> gameObjectList = new List<GameObject>();
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

    private void Search(InputField inputField)
    {
        searchedWord = inputField.text;
        FindWord(searchedWord);
    }

    private void FindWord(string word)
    {
        if (wordsDict.ContainsKey(word))
        {
            foreach (var item in gameObjectList)
            {
                Destroy(item);
            }
            if (wordsDict.TryGetValue(word, out string value))
            {
                GameObject wordItem = Instantiate(prefab, content);
                wordItem.GetComponent<WordItem>().Intantiate(word, value, ShowWord, EditWord, DeleteWord);
                gameObjectList.Add(wordItem);
            }
        }
    }

    public void ClearContent()
    {
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
        EnglishDictionaryManager.DeleteWord(word);
        ClearContent();
    }
}
