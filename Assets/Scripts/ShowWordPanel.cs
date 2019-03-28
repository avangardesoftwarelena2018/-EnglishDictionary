using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWordPanel : MonoBehaviour
{
    [SerializeField]
    private Text wordText = null;
    [SerializeField]
    private Text definitionText = null;

    public string Word
    {
        set
        {
            wordText.text = value;
        }
    }

    public string Definition
    {
        set
        {
            definitionText.text = value;
        }
    }
}
