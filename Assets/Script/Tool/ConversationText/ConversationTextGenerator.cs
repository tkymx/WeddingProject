using UnityEngine;
using System.Collections;
using System.Linq;

public class ConversationTextGenerator
{
    public static ConversationText GenerateText(string script)
    {
        if (string.IsNullOrEmpty(script)) {
            return new ConversationText();
        }

        var splits = script.Split(',');
        if (splits.Count() > 2)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog("GenerateText失敗", "要素数が違います " + script + " : " + splits.Count(), "無視する");
#endif
        }

        var convText = new ConversationText();

        if (splits.Count() == 2) {

            int charaIndex = 0;
            if(!int.TryParse(splits[0], out charaIndex)) {
#if UNITY_EDITOR
                UnityEditor.EditorUtility.DisplayDialog("GenerateText失敗", "int ではありません " + script + " : " + splits.Count(), "無視する");
#endif
            }
            string text = splits[1].TrimStart('[').TrimEnd(']');

            convText.charaIndex = charaIndex - 1;
            convText.text = text;
        } 
        else if (splits.Count() == 1) {
            string text = splits[0].TrimStart('\n').TrimStart('[').TrimEnd(']');

            convText.charaIndex = -1;
            convText.text = text;
        }

        return convText;
    }

    public static ConversationTextGroup GenerateTextGroup(string llPath, string lrPath, string rlPath, string rrPath, string llName, string lrName, string rlName, string rrName, string scriptGroup)
    {   
        var convTexts = scriptGroup.Split(':').Where(text => !string.IsNullOrEmpty(text));

        var convTextGroup = new ConversationTextGroup();
        convTextGroup.CharaNames = new string[4];
        convTextGroup.CharaNames[0] = llName;
        convTextGroup.CharaNames[1] = lrName;
        convTextGroup.CharaNames[2] = rlName;
        convTextGroup.CharaNames[3] = rrName;
        convTextGroup.CharaPaths = new string[4];
        convTextGroup.CharaPaths[0] = llPath;
        convTextGroup.CharaPaths[1] = lrPath;
        convTextGroup.CharaPaths[2] = rlPath;
        convTextGroup.CharaPaths[3] = rrPath;
        convTextGroup.Texts = convTexts.Select(GenerateText).ToList();

        return convTextGroup;
    }

}
