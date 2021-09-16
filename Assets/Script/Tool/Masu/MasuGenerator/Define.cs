using System.Collections.Generic;

public struct MapStruxt
{
    public List<string> ids;
}

public struct MasuStruct
{
    public string id;
    public string type;
    public string charaName1;
    public string charaName2;
    public string charaName3;
    public string charaName4;
    public string ConversationText;
    public string quizeText;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public int answerIndex;
    public string ConversationAfterText;
    public int answerPoint;
}

public class MasuDefine
{
    public static float startMasuPositionX = -0.19f;
    public static float startMasuPositionY = -0.7f;
    public static float intervalMasuheight = 0.4f;

    public static float GetMasuPositoinY(int currentMasuIndex) 
    {
        return MasuDefine.startMasuPositionY + MasuDefine.intervalMasuheight * currentMasuIndex;
    }
}
