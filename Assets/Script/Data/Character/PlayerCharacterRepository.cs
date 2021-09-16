using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class PlayerCharacterRepository
{
    private static List<PlayerCharacterModel> playerCharacterModels = new List<PlayerCharacterModel>();

    public static void InitializeFromArray(string[][] characterArray)
    {
        if (characterArray.Length <= 0)
        {
            Debug.LogErrorFormat("キャラクターマスターデータの読み込みができていません。");
            return;
        }

        playerCharacterModels.Clear();

        int length = characterArray.Where(array => array.Length >= 6).Count();
        foreach (var infoArray in characterArray.Where(array => array.Length >= 6))
        {
            playerCharacterModels.Add(new PlayerCharacterModel(
                infoArray[0],
                infoArray[1],
                infoArray[2],
                infoArray[3],
                infoArray[4],
                infoArray[5],
                IsGet(infoArray[0]) 
            ));
        }
    }

    public static string CharaNameToPathMap(string name)
    {
        if (name == "") {
            return "";
        }

        var findedModel = playerCharacterModels.Find(model => model.Name == name);
        if (findedModel == null) {
            Debug.LogError(name + "がいません");
        }
        return findedModel.ImageName;
    }

    public static List<PlayerCharacterModel> GetAll()
    {        
        return playerCharacterModels;
    }

    private const string FootPrintGET = "Get";

    public static void SaveGetStatus(string characterName) 
    {
        PlayerPrefs.SetInt(characterName + FootPrintGET, 1);
    }

    public static bool IsGet(string characterName)
    {
        return PlayerPrefs.GetInt(characterName + FootPrintGET, 0) == 1;
    }
}
