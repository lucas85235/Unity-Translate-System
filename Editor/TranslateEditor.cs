using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

namespace MadeInHouse.Translate
{
    public class TranslateEditor : EditorWindow
    {
        private Enum languageSelected = Languages.English;

        private int mainToolbar = 0;
        private int languagesToolbar = 0;
        private int selectedLanguage = 0;
        private string[] bars = new string[] { "Language", "Keys" };

        private LanguageList m_languageList;

        [MenuItem("Window/Translate")]
        private static void ShowWindow()
        {
            GetWindow<TranslateEditor>("Translate");
        }

        private void OnGUI()
        {
            m_languageList = Resources.Load<LanguageList>("Languages");

            GUILayout.Label("\nTranslate System\n", EditorStyles.boldLabel);
            mainToolbar = GUILayout.Toolbar(mainToolbar, bars);

            switch (mainToolbar)
            {
                case 0:
                    LanguagesBar();
                    break;
                case 1:
                    KeysBar();
                    break;
            }
        }

        private void LanguagesBar()
        {
            GUILayout.Label("\nLanguages List\n", EditorStyles.boldLabel);

            LanguageList obj = m_languageList;
            SerializedObject serializedObject = new UnityEditor.SerializedObject(obj);
            SerializedProperty serializedPropertyMyInt = serializedObject.FindProperty("languages");
            EditorGUILayout.PropertyField(serializedPropertyMyInt);

            List<string> langList = new List<string>();
            foreach (var language in m_languageList.languages)
                langList.Add(language.ToString());

            LineSpace();
            selectedLanguage = EditorGUILayout.Popup("Selected Language", selectedLanguage, langList.ToArray());
            languageSelected = (Languages)selectedLanguage;

            if (GUILayout.Button("Update Language Selected"))
            {
                LanguageManager.LoadLocazidedText(languageSelected.ToString());
            }
        }

        private void KeysBar()
        {
            LanguageFilesHandle();

            string[] langBars = new string[m_languageList.languages.Length];

            for (int i = 0; i < m_languageList.languages.Length; i++)
                langBars[i] = m_languageList.languages[i].ToString();

            languagesToolbar = GUILayout.Toolbar(languagesToolbar, langBars, EditorStyles.toolbarButton);

            switch (languagesToolbar)
            {
                default:
                    ItemsHandle();
                    break;
            }

            if (GUILayout.Button("Update Other Language Keys With This Keys"))
            {
                if (m_languageList.languages.Length == 0)
                {
                    Debug.LogError("Add language to list in language tab");
                    return;
                }

                var mainData = Resources.Load<LanguageData>(m_languageList.languages[languagesToolbar].ToString());
                var mainItems = mainData.language.items;

                foreach (var languages in m_languageList.languages)
                {
                    LanguageData tempData = Resources.Load<LanguageData>(languages.ToString());
                    var tempValues = tempData.language.items;

                    if (mainData != tempData)
                    {
                        Debug.Log(languages.ToString());
                        tempData.language.items = new List<LanguageItem>();

                        for (int i = 0; i < mainItems.Count; i++)
                        {
                            string value = "";

                            if (i < tempValues.Count)
                            {
                                value = tempValues[i].value;
                            }

                            LanguageItem item = new LanguageItem(mainItems[i].key, value);
                            tempData.language.items.Add(item);

                            Debug.Log("Add Item");
                        }
                    }
                }
            }
        }

        private void ItemsHandle()
        {
            if (m_languageList.languages.Length == 0)
            {
                Debug.LogError("Add language to list in language tab");
                return;
            }
            
            var fileName = m_languageList.languages[languagesToolbar].ToString();
            var obj = Resources.Load<LanguageData>(fileName);
            SerializedObject serializedObject = new SerializedObject(obj);
            SerializedProperty serializedPropertyMyInt = serializedObject.FindProperty("language");
            EditorGUILayout.PropertyField(serializedPropertyMyInt);
        }

        private void LanguageFilesHandle()
        {
            var languageList = m_languageList.languages;

            for (int i = 0; i < languageList.Length; i++)
            {
                LanguageData file = Resources.Load<LanguageData>(languageList[i].ToString());

                if (file == null)
                {
                    var data = LoadLanguageFile(languageList[i].ToString());

                    // string loaderData = JsonUtility.ToJson(data.langItems);
                    // var readJson = JsonUtility.FromJson<LanguageItems>(loaderData);
                }
            }
        }

        private LanguageData LoadLanguageFile(string fileName)
        {
            var filePath = "Assets/Resources/" + fileName + ".asset";

            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");

            LanguageData data = new LanguageData();
            AssetDatabase.CreateAsset(data, filePath);

            return data;
        }

        private void LineSpace() => GUILayout.Label("\n", EditorStyles.boldLabel);
    }
}

#endif
