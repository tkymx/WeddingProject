using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Separate text.
/// 文章の分割を行う。インスタンスで指定したテキストをSeparate を実行することで分割
/// </summary>
public class SeparateText
{
    private const int SeparateIndex = 30 * 2;

    private string text = "";
    private List<string> texts;
    private int currentIndex = 0;

    public SeparateText()
    {
    }

    public void Separate(string text)
    {
        this.text = text;
        this.currentIndex = 0;

        var currentSeparateIndex = 0;
        texts = new List<string>();

        // 分割できるまで繰り返す。
        for (;;){

            var restTextCount = text.Length - currentSeparateIndex;

            // SepareteIndex 数で分割する
            var separetedText = text.Substring(currentSeparateIndex, Mathf.Min(SeparateIndex, restTextCount));

            // 出力テキストに足し合わせる
            texts.Add(separetedText);

            // テキストのインデックスを足し合わせる
            currentSeparateIndex += separetedText.Length;

            // 文字が取得できなかったときはやめる
            if (separetedText.Length != SeparateIndex) {
                break;
            }
        }
    }

    public string GetCurrentText()
    {
        return texts[currentIndex];
    }

    public void NextText()
    {
        currentIndex++;
    }

    public bool IsNextFinal()
    {
        return currentIndex + 1 == texts.Count;
    }

}
