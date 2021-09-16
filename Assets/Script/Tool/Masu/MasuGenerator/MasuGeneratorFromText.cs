using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

public class MasuGeneratorFromText : IMasuGenerator
{
    string mapText;
    string masuText;

    public MasuGeneratorFromText(string mapText, string masuText)
    {
        this.mapText = mapText;
        this.masuText = masuText;
    }

    public void Generate(GameObject masuGroup)
    {
        MasuGenerator.GenerateFromText(mapText, masuText, masuGroup);
    }

    private class MasuGenerator
    {
        public static void GenerateFromText(string mapText, string masuText, GameObject root)
        {
            var mapStruct = ParseMap(mapText);
            var masuStruct = ParseMasu(masuText);

            // Masu.Type すべての要素のPrefab を最初に読み込み
            Dictionary<string, GameObject> masuPrefabs = new Dictionary<string, GameObject>();
            foreach (Masu.Type type in Enum.GetValues(typeof(Masu.Type)))
            {
                masuPrefabs[type.ToString()] = ResourceManager.GetMasuPrefab(type);
            }

            int currentMasuIndex = 0;
            foreach (var id in mapStruct.ids)
            {
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
                masuObject.transform.position = new Vector3(0, MasuDefine.GetMasuPositoinY(currentMasuIndex), 0);

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

        static MapStruxt ParseMap(string mapText)
        {
            MapStruxt mapStruxt = new MapStruxt();
            mapStruxt.ids = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(mapText));

            // Map
            XmlNode root = xmlDoc.FirstChild;

            XmlNodeList masuList = xmlDoc.GetElementsByTagName("Masu");
            for (int masuIndex = 0; masuIndex < masuList.Count; masuIndex++)
            {
                var masu = masuList[masuIndex];
                mapStruxt.ids.Add(masu.Attributes["id"].Value);
            }

            return mapStruxt;
        }

        static Dictionary<string, MasuStruct> ParseMasu(string masuText)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(masuText));

            // Masus
            XmlNode root = xmlDoc.FirstChild;

            Dictionary<string, MasuStruct> masuStructDictionary = new Dictionary<string, MasuStruct>();
            XmlNodeList masuList = xmlDoc.GetElementsByTagName("Masu");
            for (int masuIndex = 0; masuIndex < masuList.Count; masuIndex++)
            {
                var masu = masuList[masuIndex];
                var masuStruct = new MasuStruct();
                masuStruct.id = masu.Attributes["id"].Value;
                masuStruct.type = masu.Attributes["type"].Value;
                masuStruct.charaName1 = masu.Attributes["chara1"].Value;
                masuStruct.charaName2 = masu.Attributes["chara2"].Value;
                masuStruct.charaName3 = masu.Attributes["chara3"].Value;
                masuStruct.charaName4 = masu.Attributes["chara4"].Value;
                masuStruct.ConversationText = masu.Attributes["convText"].Value;
                masuStruct.quizeText = masu.Attributes["quizeText"].Value;
                masuStruct.answer1 = masu.Attributes["ans1"].Value;
                masuStruct.answer2 = masu.Attributes["ans2"].Value;
                masuStruct.answer3 = masu.Attributes["ans3"].Value;
                masuStruct.answer4 = masu.Attributes["ans4"].Value;
                masuStruct.answerIndex = int.Parse(masu.Attributes["ansIndex"].Value);
                masuStruct.answerPoint = int.Parse(masu.Attributes["ansPoint"].Value);
                masuStructDictionary.Add(masuStruct.id, masuStruct);
            }

            return masuStructDictionary;
        }
    }
}