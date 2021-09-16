using System.Collections.Generic;

public class Story
{
    public List<string> MapName { get; set; }
    public List<string> MasuName { get; set; }

    public int StoryCount()
    {
        return MapName.Count;
    }
}
