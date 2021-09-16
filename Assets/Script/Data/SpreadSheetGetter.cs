using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Linq;
using System.Collections.Generic;
using System;


public class SpreadSheetGetter
{
    // sheetId に公開中のファイルのIDを指定して、指定のシート名のデータを配列で取得
    public static IEnumerator GetSheetAsync(string sheetId, string sheetName, Action<string[][]> callback, GameObject parentErrorWindow)
    {
        var apiKey = "AIzaSyC - 4xy7aQ7kuM63L7tnmUIrFn0PjjjeU50";
        var range = sheetName + "!A:AZ";
        yield return GetSheetAsync(apiKey, sheetId, range, callback, parentErrorWindow);
    }

    private static string[][] GetSheetData(string text)
    {
        var json = MiniJSON.Json.Deserialize(text);
        var rows = (List<object>)((Dictionary<string, object>)json)["values"];

        var cells = rows.Cast<List<object>>()
            .Select(_ => _.Cast<string>().ToArray())
            .ToArray();

        return cells;
    }

    private static IEnumerator GetSheetAsync(string apiKey, string sheetId, string range, Action<string[][]> callback, GameObject parentErrorWindow)
    {
        bool isDownloadFinish = false;
        bool isVisibleErrorWindow = false;

        while(!isDownloadFinish) {
            if (isVisibleErrorWindow) {
                yield return null;
                continue;
            }
            var api = "https://sheets.googleapis.com/v4/spreadsheets/" + sheetId + "/values/" + range + "?key=" + apiKey;
            using (var request = UnityWebRequest.Get(api))
            {
                yield return request.SendWebRequest();
                if (request.isHttpError || request.isNetworkError) 
                {
                    isVisibleErrorWindow = true;
                    ShowErrorWindow(parentErrorWindow, () =>
                    {
                        isVisibleErrorWindow = false;
                    });
                    continue;
                }
                callback(GetSheetData(request.downloadHandler.text));
                isDownloadFinish = true;
            }            
            yield return null;
        }
    }

    private static void ShowErrorWindow(GameObject parent, Action onOK)
    {
        var prefab = ResourceManager.GetPrefab("UI/ConnectionError");
        var instaneGameObject = GameObject.Instantiate(prefab);
        instaneGameObject.transform.SetParent(parent.transform, false);

        var connectionErrorWindow = instaneGameObject.AddComponent<ConnectionErrorWindow>();
        connectionErrorWindow.Initialize();
        connectionErrorWindow.retryButtonEvent = () =>
        {
            onOK();
            GameObject.Destroy(instaneGameObject);
            connectionErrorWindow = null;
        };
    }
}
