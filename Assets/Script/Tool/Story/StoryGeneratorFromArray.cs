using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryGeneratorFromArray : IStoryGenerator
{
    public const string TakuSheedId = "1nSNpwBfeXpYUZHW8fOVqQJJ-7S1-ZO-liUxPNaBKpjs";
    public const string OtherSheedId = "1Bz-WZyGjL5esM-VDppMCfSi0Z7zytVmDg6v1ynB6S0s";
    public const string HiguSheedId = "1gK6-tQT-A1Uck_VXtUCXVGnp4Zs_rQmqsk35SSfBi4g";

    string storySheedId = "";
    string storySheetName = "ChapterId";

    List<Chapter> chapters = new List<Chapter>();
    List<string> chapterBackgrounds = new List<string>();
    bool isLoaded = false;

    public StoryGeneratorFromArray(string storySheedId)
    {
        this.storySheedId = storySheedId;
        isLoaded = false;
    }

    struct pushed {
        public string chapterId;
        public string chapterBackgroundPath;
    }

    Queue<pushed> pushedChapterId = new Queue<pushed>();

    public IEnumerator LoadAsync(GameObject parentErrorWindow)
    {
        isLoaded = false;

        yield return SpreadSheetGetter.GetSheetAsync(
            storySheedId,
            storySheetName,
            parseArray,
            parentErrorWindow
        );

        while(pushedChapterId.Count != 0) {

            pushed chapterPushed = pushedChapterId.Dequeue();

            string[][] tempMapArray = null;
            string[][] tempMasuArray = null;

            yield return SpreadSheetGetter.GetSheetAsync(
                chapterPushed.chapterId,
                "Map",
                (string[][] mapArray) => tempMapArray = mapArray,
                parentErrorWindow
            );

            yield return SpreadSheetGetter.GetSheetAsync(
                chapterPushed.chapterId,
                "Masu",
                (string[][] masuArray) => tempMasuArray = masuArray,
                parentErrorWindow
            );

            chapters.Add(new Chapter(new MasuGeneratorFromArray(tempMapArray, tempMasuArray)));
            chapterBackgrounds.Add(chapterPushed.chapterBackgroundPath);
        }

        isLoaded = true;
    }

    private void parseArray(string[][] textArray)
    {
        if (textArray.Length < 2) {
            return;
        }

        pushedChapterId.Clear();

        for (int index = 1; index < textArray.Length; index++) 
        {
            string chapterId = textArray[index][0];
            string backgtoundPath = textArray[index][1];
            pushedChapterId.Enqueue(new pushed(){
                chapterId = chapterId,
                chapterBackgroundPath = backgtoundPath
            });
        }
    }

    // 読み込みが終了したか？
    public bool IsLoaded {
        get { return isLoaded; }
    }

    public int GetChapterCount()
    {
        Debug.Assert(isLoaded, "読み込みが終わっていません");
        return chapters.Count;
    }

    public Chapter GetChapter(int chapterIndex)
    {
        Debug.Assert(isLoaded, "読み込みが終わっていません");
        return chapters[chapterIndex];
    }

    public string GetChapterBackground(int chapterIndex)
    {
        Debug.Assert(isLoaded, "読み込みが終わっていません");
        return chapterBackgrounds[chapterIndex];
    }
}
