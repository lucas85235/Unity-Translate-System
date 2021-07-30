using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MadeInHouse.Translate
{
    public class LanguageLoadAfterReady : MonoBehaviour
    {
        public string loaderScene;
        public bool loadSaveLanguage = true;

        private IEnumerator Start()
        {
            if (loadSaveLanguage)
            {
                if (PlayerPrefs.HasKey(LanguageManager.SaveLanguageKey))
                {
                    LanguageManager.LoadLocazidedText();
                }
            }

            while (!LanguageManager.IsReady)
            {
                yield return null;
            }

            SceneManager.LoadScene(loaderScene);
        }
    }
}
