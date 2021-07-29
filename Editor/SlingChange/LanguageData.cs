using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LanguageData", menuName = "ScriptableObjects/LanguageData", order = 2)]
public class LanguageData : ScriptableObject
{
    public LanguageItems langItems = new LanguageItems();
}

[System.Serializable]
public class LanguageItems
{
    public List<LanguageItem> items = new List<LanguageItem>();
}

[System.Serializable]
public struct LanguageItem
{
    public string key;
    public string value;

    public void SetKey(string key)
    {
        this.key = key; 
    }
    
    public LanguageItem(string key, string value)
    {
        this.key = key;
        this.value = value;
    }
}
