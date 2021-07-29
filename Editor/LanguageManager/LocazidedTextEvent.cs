using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocazidedTextEvent : MonoBehaviour
{
    public Languages language;

    private void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            StartCoroutine(ChangeLanguage());
        });
    }

    private IEnumerator ChangeLanguage()
    {
        if (LanguageManager.Instance.selectLanguage == language) yield break;

        LanguageManager.Instance.selectLanguage = language;

        yield return null;

        /*if (language == Languages.Portuguese)
            PlayerPrefs.SetString(LanguageManager.Instance.SaveLanguageKey, LanguageManager.Instance.languageOptions[0]);
        else if (language == Languages.English)
            PlayerPrefs.SetString(LanguageManager.Instance.SaveLanguageKey, LanguageManager.Instance.languageOptions[1]);*/

        PlayerPrefs.Save();

        // LevelLoader.Instance.LoadLevel("Loading");
    }
}
