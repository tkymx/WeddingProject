using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using UnityEditor;

// https://developers.google.com/sheets/guides/concepts?hl=ja

public class SpreadSheet
{
    public static void TempAction(string[][] value) 
    {
        Debug.Log("終わり");
    }

    [MenuItem("Test/ClearPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Test/TestSpreadSheet")]
    public static void TestSpreadSheet()
    {
        var emurator = LoadCoroutine(
            "AIzaSyC-4xy7aQ7kuM63L7tnmUIrFn0PjjjeU50",
            "1nSNpwBfeXpYUZHW8fOVqQJJ-7S1-ZO-liUxPNaBKpjs",
            "A:AE"
        );
        while(emurator.MoveNext()) {
            Debug.Log("now loading");
        }
    }

    public static IEnumerator LoadCoroutine(string apiKey, string sheetId, string range)
    {
        var api = "https://sheets.googleapis.com/v4/spreadsheets/" + sheetId + "/values/" + range + "?key=" + apiKey;
        using (var request = UnityWebRequest.Get(api))
        {
            yield return request.SendWebRequest();
            if (request.isHttpError)
            {
                yield break;
            }
            if (request.isNetworkError)
            {
                yield break;
            }
            var text = request.downloadHandler.text;
            Debug.Log(text);
        }
    }
}