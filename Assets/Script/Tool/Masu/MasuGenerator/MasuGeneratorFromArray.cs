using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

public class MasuGeneratorFromArray : IMasuGenerator
{
    string[][] mapArray;
    string[][] masuArray;

    public MasuGeneratorFromArray(string[][] mapArray, string[][] masuArray)
    {
        this.mapArray = mapArray;
        this.masuArray = masuArray;
    }

    public void Generate(GameObject masuGroup)
    {
        GenerateFromArray(mapArray, masuArray, masuGroup);
    }

    public static void GenerateFromArray(string[][] mapArray, string[][] masuArray, GameObject root)
    {
        var mapStruct = ParseMap(mapArray);
        var masuStruct = ParseMasu(masuArray);

        // Masu.Type すべての要素のPrefab を最初に読み込み
        Dictionary<string, GameObject> masuPrefabs = new Dictionary<string, GameObject>();
        foreach (Masu.Type type in Enum.GetValues(typeof(Masu.Type)))
        {
            masuPrefabs[type.ToString()] = ResourceManager.GetMasuPrefab(type);
        }

        int currentMasuIndex = 0;
        foreach(var id in mapStruct.ids) {
            if (!masuStruct.ContainsKey(id))
            {
#if UNITY_EDITOR
                UnityEditor.EditorUtility.DisplayDialog("idがありません", id.ToString(), "無視");
#endif
            }

            var masu = masuStruct[id];

            // マスの生成
            var masuObject = GameObject.Instantiate(masuPrefabs[masu.type]);
            masuObject.transform.SetParent(root.transform, false);

            // マスの位置設定
            masuObject.transform.position = new Vector3(MasuDefine.startMasuPositionX, MasuDefine.GetMasuPositoinY(currentMasuIndex), 0);

            // マスのクイズを設定
            var masuComponent = masuObject.GetComponent<Masu>();
            masuComponent.Quiz = Quiz.SetValue(
                masu.quizeText,
                masu.answer1,
                masu.answer2,
                masu.answer3,
                masu.answer4,
                masu.answerIndex,
                masu.answerPoint
            );

            // マスのテキスト　クイズ前
            masuComponent.ConversationTextGroup = ConversationTextGenerator.GenerateTextGroup(
                PlayerCharacterRepository.CharaNameToPathMap(masu.charaName1),
                PlayerCharacterRepository.CharaNameToPathMap(masu.charaName2),
                PlayerCharacterRepository.CharaNameToPathMap(masu.charaName3),
                PlayerCharacterRepository.CharaNameToPathMap(masu.charaName4),
                masu.charaName1,
                masu.charaName2,
                masu.charaName3,
                masu.charaName4,
                masu.ConversationText
            );

            // マスのテキスト　クイズ後
            masuComponent.AfterConversationTextGroup = ConversationTextGenerator.GenerateTextGroup(
                PlayerCharacterRepository.CharaNameToPathMap(masu.charaName1),
                PlayerCharacterRepository.CharaNameToPathMap(masu.charaName2),
                PlayerCharacterRepository.CharaNameToPathMap(masu.charaName3),
                PlayerCharacterRepository.CharaNameToPathMap(masu.charaName4),
                masu.charaName1,
                masu.charaName2,
                masu.charaName3,
                masu.charaName4,
                masu.ConversationAfterText
            );

            currentMasuIndex++;
        }
    }

    static MapStruxt ParseMap(string[][] mapArray)
    {
        MapStruxt mapStruxt = new MapStruxt();
        mapStruxt.ids = new List<string>();

        for (int masuIndex = 1; masuIndex < mapArray.Length; masuIndex++)
        {
            mapStruxt.ids.Add(mapArray[masuIndex][0]);
        }

        return mapStruxt;
    }

    static Dictionary<string,MasuStruct> ParseMasu(string[][] masuArray)
    {
        Dictionary<string, MasuStruct> masuStructDictionary = new Dictionary<string, MasuStruct>();

        for (int masuIndex = 1; masuIndex < masuArray.Length; masuIndex++) {

            var masuStruct = new MasuStruct();
            masuStruct.id                   = GetStringFromArray(masuArray, masuIndex, 0);
            masuStruct.type                 = GetStringFromArray(masuArray, masuIndex, 1, "Normal");
            masuStruct.charaName1           = GetStringFromArray(masuArray, masuIndex, 2);
            masuStruct.charaName2           = GetStringFromArray(masuArray, masuIndex, 3);
            masuStruct.charaName3           = GetStringFromArray(masuArray, masuIndex, 4);
            masuStruct.charaName4           = GetStringFromArray(masuArray, masuIndex, 5);
            masuStruct.ConversationText     = GetStringFromArray(masuArray, masuIndex, 6);
            masuStruct.quizeText            = GetStringFromArray(masuArray, masuIndex, 7);
            masuStruct.answer1              = GetStringFromArray(masuArray, masuIndex, 8);
            masuStruct.answer2              = GetStringFromArray(masuArray, masuIndex, 9);
            masuStruct.answer3              = GetStringFromArray(masuArray, masuIndex, 10);
            masuStruct.answer4              = GetStringFromArray(masuArray, masuIndex, 11);
            masuStruct.answerIndex          = GetIntFromArray(masuArray, masuIndex, 12);
            masuStruct.ConversationAfterText= GetStringFromArray(masuArray, masuIndex, 13);
            masuStruct.answerPoint          = GetIntFromArray(masuArray, masuIndex, 14);
            masuStructDictionary.Add(masuStruct.id,masuStruct);
        }

        return masuStructDictionary;
    }

    static string GetStringFromArray(string[][] masuArray, int firstIndex, int secondIndex, string defaultText="")
    {
        if (masuArray.Length <= firstIndex) {
            return defaultText;
        }

        if (masuArray[firstIndex].Length <= secondIndex) {
            return defaultText;
        }

        if (string.IsNullOrEmpty(masuArray[firstIndex][secondIndex])) {
            return defaultText;
        }

        return masuArray[firstIndex][secondIndex];
    }

    static int GetIntFromArray(string[][] masuArray, int firstIndex, int secondIndex)
    {
        if (masuArray.Length <= firstIndex)
        {
            return 0;
        }

        if (masuArray[firstIndex].Length <= secondIndex)
        {
            return 0;
        }

        return int.Parse(masuArray[firstIndex][secondIndex]);
    }
}
