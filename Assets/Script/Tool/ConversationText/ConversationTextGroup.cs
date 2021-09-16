using System.Collections.Generic;

// テキストの集合
public class ConversationTextGroup
{
    public string[] CharaPaths;
    public string[] CharaNames;
    public List<ConversationText> Texts;

    public string GetCharaName(int index) 
    {
        if (index < 0 || index >= CharaNames.Length) {
            return "";
        }

        return CharaNames[index];
    }
}