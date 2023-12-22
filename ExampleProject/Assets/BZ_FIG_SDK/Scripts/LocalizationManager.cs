// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.Localization.Settings;
// using UnityEngine.Localization.Tables;
// using UnityEngine.ResourceManagement.AsyncOperations;

namespace BZ_FIG_SDK.Scripts
{
    // -------------------------------------------------------
    // REMOVE COMMENT IF YOU WANT TO USE ---------------------
    // REQUIRED TO INSTALL "Localization" PACKAGE ------------
    // -------------------------------------------------------
    public class LocalizationManager : Singleton<LocalizationManager>
    {
        // private bool _active;
        //
        // private void Awake()
        // {
        //     var id = GetLocale();
        //     ChangeLocale(id);
        // }
        //
        // private IEnumerator SetLocale(int localeID, Action callback = null)
        // {
        //     _active = true;
        //     yield return LocalizationSettings.InitializationOperation;
        //     LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        //     PlayerPrefs.SetInt("LocaleKey", localeID);
        //     _active = false;
        //
        //     yield return new WaitForSeconds(0.2f);
        //     callback?.Invoke();
        //     MessageBroker.Default.Publish(new ChangeLanguageEvent());
        // }
        //
        // public int GetLocale()
        // {
        //     return PlayerPrefs.GetInt("LocaleKey", 1);
        // }
        //
        // public void ChangeLocale(int localeID, Action callback = null)
        // {
        //     if (_active) return;
        //
        //     StartCoroutine(SetLocale(localeID, callback));
        // }
        //
        // public string GetLocaleName(int localeID)
        // {
        //     return LocalizationSettings.AvailableLocales.Locales[localeID].name;
        // }
        //
        // public bool IsCurrentLocale(int localeID)
        // {
        //     return localeID == GetLocale();
        // }
        //
        // public string Translate(string key, string tableName = "Translates")
        // {
        //     var table = GetLocalizationTable(tableName).Result;
        //     if (table == null) return key;
        //
        //     // Get a localized string by key.
        //     foreach (var entry in table)
        //         if (entry.Value.Key == key)
        //             return entry.Value.GetLocalizedString();
        //
        //     return key;
        //
        //     // return LocalizationSettings.StringDatabase.GetLocalizedString(tableName, key);
        // }
        //
        // private AsyncOperationHandle<StringTable> GetLocalizationTable(string tableName)
        // {
        //     // Get the string table from the LocalizationSettings.
        //     return LocalizationSettings.StringDatabase.GetTableAsync(tableName);
        // }
    }
}