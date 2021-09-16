using UnityEngine;
using System.Collections;

public class ResourceManager
{

    public static Sprite GetSprite(string path)
    {
        var sprite = Resources.Load<Sprite>(path);
        if (sprite == null)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog("ファイルが存在しません", path, "無視する");
#endif
        }
        return sprite;
    }

    public static GameObject GetMasuPrefab(Masu.Type type)
    {
        var asset = Resources.Load<GameObject>("Masu/" + type.ToString());
        if (asset == null)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog("ファイルが存在しません", type.ToString(), "無視する");
#endif
        }
        return asset;
    }

    public static string GetText(string name)
    {
        var asset = Resources.Load<TextAsset>("Text/" + name);
        if (asset == null)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog("ファイルが存在しません", name, "無視する");
#endif
        }
        return asset.text;
    }

    public static GameObject GetStoryTellerPrefab()
    {
        return GetPrefab("PermanentPrefab/StoryTeller");
    }

    public static GameObject GetPointStorePrefab()
    {
        return GetPrefab("PermanentPrefab/PointStore");
    }

    public static GameObject GetPrefab(string path)
    {
        var asset = Resources.Load<GameObject>(path);
        if (asset == null)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog("ファイルが存在しません", path, "無視する");
#endif
        }
        return asset;
    }
}
