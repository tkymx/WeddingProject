using UnityEngine;
using System.Collections;

public class LoadingTouchGuardDialogGenerator
{
    public enum Type {
        Normal,
        Balck
    }

    public static string GetFile(Type type) 
    {
        if (type == Type.Normal) {
            return "System/Loading";
        }
        if (type == Type.Balck)
        {
            return "System/Loading_black";
        }

        return "System/Loading";
    }

    public static LoadingTouchGuardDialogPresenter Generate(GameObject root, Type type = Type.Normal)
    {
        var prefab = ResourceManager.GetPrefab(GetFile(type));
        var instance = GameObject.Instantiate(prefab);
        instance.transform.SetParent(root.transform, false);
        return instance.GetComponent<LoadingTouchGuardDialogPresenter>();
    }
}
