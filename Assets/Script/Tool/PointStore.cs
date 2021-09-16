using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Story teller.
/// タイトルなどの初期シーンには必ず配置する必要がある
/// </summary>
public class PointStore : MonoBehaviour
{
    private static PointStore instance;
    public static PointStore Instance
    {
        get
        {
            if (instance == null)
            {

                instance = (PointStore)FindObjectOfType(typeof(PointStore));
                if (instance == null)
                {

                    instance = Instantiate(ResourceManager.GetPointStorePrefab()).GetComponent<PointStore>();
                }
            }
            return instance;
        }
    }

    [RuntimeInitializeOnLoadMethod()]
    static void Init()
    {
        DontDestroyOnLoad(PointStore.Instance);
    }

    // 現在のポイントの初期化
    public void InitializeCurrentGamePoint()
    {
        beforeIncrementScore = MaxGamePoint;
        currentGamePoint = 0;
    }

    // 中断データでの初期化
    public void InitializePauseGamePoint(int reloadPausePoint)
    {
        InitializeCurrentGamePoint();
        currentGamePoint = reloadPausePoint;
    }

    // 最大ゲームポイント
    public int MaxGamePoint{
        get{
            return PlayerPrefs.GetInt("MaxGamePoint", 0);
        }
    }

    // ゲーム中の総合ポイント
    private int currentGamePoint;
    public int CurrentGamePoint{
        get {
            return currentGamePoint; 
        }
        set {
            currentGamePoint = value;
        }
    }

    // 加算前のスコア（リザルトで使用可能）
    private int beforeIncrementScore;
    public int BeforeIncrementScore {
        get {
            return beforeIncrementScore;
        }
    }

    // ゲーム中のポイントを加算する
    public void AddPoint(int point)
    {
        CurrentGamePoint = CurrentGamePoint + point;
    }

    // ゲームポイントの保存
    public void StoreCurrentGamePointToMaxGamePoint()
    {
        // 加算前のポイントを保存
        beforeIncrementScore = MaxGamePoint;

        // 前のポイントに加算する
        var incrementCurrentMaxGamePoint = beforeIncrementScore + currentGamePoint;

        // 保存
        PlayerPrefs.SetInt("MaxGamePoint", incrementCurrentMaxGamePoint);
    }
}