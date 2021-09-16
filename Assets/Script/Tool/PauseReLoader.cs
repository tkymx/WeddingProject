using UnityEngine;
using System.Collections;

public class PauseReLoader
{
    // ポーズ済みのデータがあるのか？
    public static bool IsPauseReload()
    {
        return PlayerPrefs.GetInt("IsPauseReload", 0) > 0;
    }

    public static void EnablePauseReload(bool enable)
    {
        PlayerPrefs.SetInt("IsPauseReload", enable ? 1 : 0);
    }

    // 初期位置
    public static int ReloadStartMasuIndex
    {
        get
        {
            return PlayerPrefs.GetInt("ReloadStartMasuIndex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("ReloadStartMasuIndex", value);
        }
    }

    // 現在のストーリー
    public static string ReloadStoryId {
        get {
            return PlayerPrefs.GetString("ReloadStoryId","");
        }
        set {
            PlayerPrefs.SetString("ReloadStoryId", value);
        }
    }

    // 現在のポイント
    public static int ReloadPoint
    {
        get
        {
            return PlayerPrefs.GetInt("ReloadPoint", 0);
        }
        set
        {
            PlayerPrefs.SetInt("ReloadPoint", value);
        }
    }

    // 現在のチャプター
    public static int ReloadChapter
    {
        get
        {
            return PlayerPrefs.GetInt("ReloadChapter", 0);
        }
        set
        {
            PlayerPrefs.SetInt("ReloadChapter", value);
        }
    }

    // 現在のアイコン
    public static string ReloadHitoPath
    {
        get
        {
            return PlayerPrefs.GetString("ReloadHitoPath", "Hito/hito");
        }
        set
        {
            PlayerPrefs.SetString("ReloadHitoPath", value);
        }
    }
}
