using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool CoroutineActive;

    void Start() //https://www.youtube.com/watch?v=qcXuvd7qSxg&list=LL&index=6&t=31s
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
    }

    public void ChangeLocale(int localeID)
    {
        if (CoroutineActive == true)
        {
            return;
        }
        StartCoroutine(SetLocale(localeID));
    }

    private IEnumerator SetLocale(int locale_ID)
    {
        CoroutineActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[locale_ID];
        PlayerPrefs.SetInt("LocaleKey", locale_ID);
        CoroutineActive = false;
    }
}
