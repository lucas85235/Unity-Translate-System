using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[ExecuteInEditMode]
public class LanguageText : MonoBehaviour
{
    private Text text;

    [Header("Text Key")]
    public string key;

    private IEnumerator Start()
    {
        text = GetComponent<Text>();    

        var lang = FindObjectOfType<LanguageManager>();
        if (!lang.IsReady()) 
        {
            yield return null;
        }

        text.text = LanguageManager.Instance.GetKeyValue(key);
        LanguageManager.Instance.OnChangeLanguage += UpdateText;
    }

    public void UpdateText() 
    {
        text.text = LanguageManager.Instance.GetKeyValue(key);
    }

    private void OnDestroy()
    {
        LanguageManager.Instance.OnChangeLanguage -= UpdateText;
    }    
}
