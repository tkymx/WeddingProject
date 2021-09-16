using UnityEngine;
using System.Collections;

public class PlayerCharacterModel
{
    private string name;
    public string Name {
        get {
            return name;
        }
    }

    private string kind;
    public string Kind
    {
        get
        {
            return kind;
        }
    }

    private string type;
    public string Type
    {
        get
        {
            return type;
        }
    }

    private string location;
    public string Location
    {
        get
        {
            return location;
        }
    }

    private string profile;
    public string Profile
    {
        get
        {
            return profile;
        }
    }

    private string imageName;
    public string ImageName
    {
        get
        {
            return imageName;
        }
    }

    private bool isGet;
    public bool IsGet
    {
        get {
            return isGet;
        }
    }

    public PlayerCharacterModel(string name, string kind, string type, string location, string profile, string imageName, bool isGet) 
    {
        this.name = name;
        this.kind = kind;
        this.type = type;
        this.location = location;
        this.profile = profile;
        this.imageName = imageName;
        this.isGet = isGet;
    }
}
