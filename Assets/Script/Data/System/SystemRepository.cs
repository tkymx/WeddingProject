using UnityEngine;
using System.Collections;

public class SystemRepository
{
    private static SystemModel systemModel;

    public static void InitializeFromArray(string[][] systemArray)
    {
        if (systemArray.Length <= 0)
        {
            Debug.LogErrorFormat("システムマスターデータの読み込みができていません。");
            return;
        }

        systemModel = null;
        systemModel = new SystemModel(
            int.Parse(systemArray[0][1]) == 1,
            systemArray[1][1]
        );
    }

    public static SystemModel Get()
    {
        return systemModel;
    }
}
