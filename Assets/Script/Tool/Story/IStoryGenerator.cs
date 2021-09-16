using UnityEngine;
using System.Collections;

public interface IStoryGenerator
{
    IEnumerator LoadAsync(GameObject parentErrorWindow);
    int GetChapterCount();
    Chapter GetChapter(int chapterIndex);
    string GetChapterBackground(int chapterIndex);
}
