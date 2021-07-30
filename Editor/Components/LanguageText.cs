using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace MadeInHouse.Translate
{
    [ExecuteInEditMode]
    public class LanguageText : MonoBehaviour
    {
        private Text text;

        [Header("Text Key")]
        public string key;

        private void Awake()
        {
            if (!LanguageManager.IsReady)
            {
                LanguageManager.LoadLocazidedText();
            }
        }

        private IEnumerator Start()
        {
            text = GetComponent<Text>();

            if (!LanguageManager.IsReady)
            {
                yield return null;
            }

            text.text = LanguageManager.GetKeyValue(key);
            LanguageManager.OnChangeLanguage += UpdateText;
        }

        public void UpdateText()
        {
            text.text = LanguageManager.GetKeyValue(key);
        }

        private void OnDestroy()
        {
            LanguageManager.OnChangeLanguage -= UpdateText;
        }
    }
}
