using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordItem : MonoBehaviour
{
    [SerializeField]
    private Text wordText = null;
    [SerializeField]
    private Button showButton = null;
    [SerializeField]
    private Button editButton = null;
    [SerializeField]
    private Button deleteButton = null;

    public void Intantiate(string word, string definition, Action<string> showCallback, Action<string> editCallback, Action<string> deleteCallback)
    {
        wordText.text = word;
        showButton.onClick.AddListener(() => showCallback(word));
        editButton.onClick.AddListener(() => editCallback(word));
        deleteButton.onClick.AddListener(() => deleteCallback(word));
    }
}
