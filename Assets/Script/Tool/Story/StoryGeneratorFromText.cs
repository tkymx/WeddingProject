using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class StoryGeneratorFromText : IStoryGenerator
{
    private class Story
    {
        public List<string> MapName { get; set; }
        public List<string> MasuName { get; set; }

        public int StoryCount()
        {
            return MapName.Count;
        }
    }

    Story currentStory;

    public IEnumerator LoadAsync(GameObject parentErrorWindow) 
    {
        yield break;
    }

    public StoryGeneratorFromText(string storyText)
    {
        currentStory = GenerateFromTextInternal(storyText);
    }

    public int GetChapterCount()
    {
        return currentStory.StoryCount();
    }

    public Chapter GetChapter(int chapterIndex)
    {
        return new Chapter(
            new MasuGeneratorFromText(
                ResourceManager.GetText(currentStory.MapName[chapterIndex]),
                ResourceManager.GetText(currentStory.MasuName[chapterIndex])
            )
        );
    }

    public string GetChapterBackground(int chapterIndex)
    {
        return "Map/kusa";
    }

    // Internal部分

    private struct StoryStruxt
    {
        public List<string> mapNames;
        public List<string> masuNames;
    }

    private static Story GenerateFromTextInternal(string storyTextPath)
    {
        var storyStruct = ParseStory(storyTextPath);

        var story = new Story();
        story.MapName = storyStruct.mapNames;
        story.MasuName = storyStruct.masuNames;

        return story;
    }

    private static StoryStruxt ParseStory(string mapText)
    {
        StoryStruxt storyStruxt = new StoryStruxt();
        storyStruxt.mapNames = new List<string>();
        storyStruxt.masuNames = new List<string>();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(mapText));

        // Map
        XmlNode root = xmlDoc.FirstChild;

        XmlNodeList chapterList = xmlDoc.GetElementsByTagName("Chapter");
        for (int chapterIndex = 0; chapterIndex < chapterList.Count; chapterIndex++)
        {
            var chapter = chapterList[chapterIndex];
            storyStruxt.mapNames.Add(chapter.Attributes["mapName"].Value);
            storyStruxt.masuNames.Add(chapter.Attributes["masuName"].Value);
        }

        return storyStruxt;
    }

    private static StoryStruxt ParseStoryFromArray(string mapText)
    {
        StoryStruxt storyStruxt = new StoryStruxt();
        storyStruxt.mapNames = new List<string>();
        storyStruxt.masuNames = new List<string>();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(mapText));

        // Map
        XmlNode root = xmlDoc.FirstChild;

        XmlNodeList chapterList = xmlDoc.GetElementsByTagName("Chapter");
        for (int chapterIndex = 0; chapterIndex < chapterList.Count; chapterIndex++)
        {
            var chapter = chapterList[chapterIndex];
            storyStruxt.mapNames.Add(chapter.Attributes["mapName"].Value);
            storyStruxt.masuNames.Add(chapter.Attributes["masuName"].Value);
        }

        return storyStruxt;
    }
}
