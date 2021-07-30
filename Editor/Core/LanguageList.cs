using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadeInHouse.Translate
{
    [CreateAssetMenu(fileName = "Languages", menuName = "ScriptableObjects/LanguageList", order = 1)]
    public class LanguageList : ScriptableObject
    {
        [HideInInspector]
        public Languages[] languages = new Languages[] { };

        public Languages[] GetList()
        {
            return languages;
        }
    }
}
