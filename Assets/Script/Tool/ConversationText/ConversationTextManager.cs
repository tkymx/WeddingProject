using UnityEngine;
using System.Collections;

public class ConversationTextManager
{
    private ConversationTextGroup conversationTextGroup = null;
    private ConversationTextGroupView conversationTextGroupView = null;
    private SeparateText separeteText = null;
    private int currentIndex = 0;


    public ConversationTextManager(ConversationTextGroup conversationTextGroup, ConversationTextGroupView conversationTextGroupView)
    {
        this.conversationTextGroup = conversationTextGroup;

        this.conversationTextGroupView = conversationTextGroupView;
        this.conversationTextGroupView.InitializeView(
            this.conversationTextGroup.CharaPaths[0],
            this.conversationTextGroup.CharaPaths[1],
            this.conversationTextGroup.CharaPaths[2],
            this.conversationTextGroup.CharaPaths[3]);

        currentIndex = 0;

        if(conversationTextGroup.Texts.Count == 0) {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog("テキストがありません ", this.conversationTextGroup.CharaPaths[0], "無視する");
#endif

        }

        this.separeteText = new SeparateText();
        this.separeteText.Separate(conversationTextGroup.Texts[currentIndex].text);
    }


    public void NextText()
    {
        if (separeteText.IsNextFinal())
        {
            // 次が終了だったら次の文字に変える
            currentIndex++;
            separeteText.Separate(conversationTextGroup.Texts[currentIndex].text);
        }
        else {
            separeteText.NextText();
        }
    }

    public bool IsNextFinal()
    {
        return currentIndex + 1 == conversationTextGroup.Texts.Count && separeteText.IsNextFinal();
    }

    public void UpdateView()
    {
        // 会話キャラの名前を取得
        var convertionCharacterName = conversationTextGroup.GetCharaName(conversationTextGroup.Texts[currentIndex].charaIndex);

        // 会話キャラを登録
        if (!string.IsNullOrEmpty(convertionCharacterName)) {
            PlayerCharacterRepository.SaveGetStatus(convertionCharacterName);
        }

        // 会話状態の設定
        conversationTextGroupView.UpdateView(
            separeteText.GetCurrentText(), 
            conversationTextGroup.GetCharaName(conversationTextGroup.Texts[currentIndex].charaIndex),
            conversationTextGroup.Texts[currentIndex].charaIndex);
    }

    public static ConversationTextManager CreateConversationTextManager(
        string llPath, string lrPath, string rlPath, string rrPath, string llName, string lrName, string rlName, string rrName, string textScript,
        ConversationTextGroupView conversationTextGroupView)
    {
        return new ConversationTextManager(
            ConversationTextGenerator.GenerateTextGroup(llPath, lrPath, rlPath, rrPath, llName, lrName, rlName, rrName, textScript),
            conversationTextGroupView
        );
    }

}
