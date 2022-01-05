using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

namespace MadeInHouse.Translate
{
    public static class LanguageManager
    {
        private static string m_missingText = "Text or key not found!";
        private const string m_saveLanguageKey = "Language";
        public static string SaveLanguageKey { get => m_saveLanguageKey; }

        private static string m_languageSelected = "English";
        public static string[] LanguageOptions { get => m_languageOptions; }
        private static string[] m_languageOptions = {
            Languages.English.ToString(),
            Languages.Portuguese.ToString(),
            Languages.Spanish.ToString(),
            Languages.German.ToString(),
            Languages.Japanese.ToString(),
            Languages.Chinese.ToString(),
            Languages.Russian.ToString(),
            Languages.French.ToString(),
            Languages.Polish.ToString(),
            Languages.Dutch.ToString(),
            Languages.Greek.ToString(),
            Languages.Italian.ToString(),
        };

        private static bool m_isReady = false;
        public static bool IsReady { get => m_isReady; }
        public static Dictionary<string, string> m_localizedText = new Dictionary<string, string>();

        /// <summary> Use this event to switch texts after language change </summary>
        public static Action OnChangeLanguage;

        /// <summary> save the language that is the same as the scriptable object name </summary>
        public static void SaveLanguage(string fileName)
        {
            m_languageSelected = fileName;
            PlayerPrefs.SetString(m_saveLanguageKey, fileName);
            PlayerPrefs.Save();
        }

        /// <summary> initialize the component and load the saved language </summary>
        private static void Initialize()
        {
            if (!PlayerPrefs.HasKey(m_saveLanguageKey))
            {
                SaveLanguage(m_languageOptions[0]);
            }

            // Load Saved Key
            m_languageSelected = PlayerPrefs.GetString(m_saveLanguageKey);
            LoadLocazidedText(m_languageSelected);
        }

        /// <summary> find and load the file with the translations </summary>
        public static void LoadLocazidedText(string fileName = "")
        {
            if (!PlayerPrefs.HasKey(m_saveLanguageKey))
            {
                Initialize();
                return;
            }

            if (fileName == "") fileName = m_languageSelected;

            m_isReady = false;
            m_localizedText = new Dictionary<string, string>();

            Debug.Log(fileName);
            LanguageData file = Resources.Load<LanguageData>(fileName);
            string toJson = JsonUtility.ToJson(file.language);
            Language loaderData = JsonUtility.FromJson<Language>(toJson);

            for (int i = 0; i < loaderData.items.Count; i++)
            {
                m_localizedText.Add(loaderData.items[i].key, loaderData.items[i].value);
            }

            SaveLanguage(fileName);
            OnChangeLanguage?.Invoke();

            Debug.Log("Data loader, dictionary contains: " + m_localizedText.Count + " entries");
            m_isReady = true;
        }

        /// <summary> returns the value defined for the key </summary>
        public static string GetKeyValue(string key)
        {
            string resul = m_missingText;

            if (m_localizedText.ContainsKey(key))
            {
                resul = m_localizedText[key];
            }
            else LoadLocazidedText();

            return resul;
        }
    }

    /// <summary> registered languages not necessarily the ones that are available </summary>
    public enum Languages
    {
        English,
        Portuguese,
        Spanish,
        German,
        Japanese,
        Chinese,
        Russian,
        French,
        Polish,
        Dutch,
        Greek,
        Italian,
    }
}
