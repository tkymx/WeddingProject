using UnityEngine;
using System.Collections;

public class SystemModel
{
    private bool isLock;
    public bool IsLock
    {
        get {
            return isLock;
        }
    }

    private string password;
    public string Password
    {
        get
        {
            return password;
        }
    }

    private bool isInitializeDone = false;
    public bool IsInitializeDone {
        get {
            return isInitializeDone;
        }
    }

    public void OK()
    {
        isInitializeDone = true;
    }

    public SystemModel(bool isLock, string password) 
    {
        this.isLock = isLock;
        this.password = password;
    }

    public bool isOk(string password) 
    {
        return this.password == password;
    }
}
