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
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();
    public Dictionary<string, string> words = new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        UpdateContent();
    }

    private void UpdateContent()
    {
        foreach (var item in instantiatedPrefabs)
        {
            Destroy(item);
        }

        for (int i = 0; i < EnglishDictionaryManager.EnglishDictionary.words.Count; i++)
        {
            GameObject uiWord = Instantiate(prefab, content);
            instantiatedPrefabs.Add(uiWord);
        }
    }

    public void Search()
    {
        string word = inputField.text;
    }

    public void Add()
    {
        string word = "word";
        string definition = "definition";
        words.Add(word, definition);
        EnglishDictionaryManager.AddWord(word, definition);
        UpdateContent();
    }
}
