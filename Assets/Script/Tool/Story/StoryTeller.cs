using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

/// <summary>
/// Story teller.
/// タイトルなどの初期シーンには必ず配置する必要がある
/// </summary>
public class StoryTeller : MonoBehaviour
{
    public class GameStartParameter
    {
        private const string defultHitoPath = "Hito/hito";

        public int StartIndex { get; set; }
        public int StartChapter { get; set; }
        public HitoResources hitoResources { get; set; }

        public GameStartParameter() {
            StartIndex = 0;
            StartChapter = 0;
            hitoResources = new HitoResources(defultHitoPath);
        }
    }

    public static GameObject ParentErrorWindow {
        get {
            // 本当は書いてはいけないがエラー時のコードなので許して
            return GameObject.Find("Canvas");
        }
    }

    private static StoryTeller instance;
    public static StoryTeller Instance
    {
        get
        {
            if (instance == null) {
                
                instance = (StoryTeller)FindObjectOfType(typeof(StoryTeller));
                if (instance == null) {

                    instance = Instantiate(ResourceManager.GetStoryTellerPrefab()).GetComponent<StoryTeller>();
                }
            }
            return instance;
        }
    }

    private string storyId;
    public string StoryId {
        get { return storyId; }
    }

    [RuntimeInitializeOnLoadMethod()]
    static void Init()
    {
        DontDestroyOnLoad(StoryTeller.Instance);

#if UNITY_EDITOR
        if (SceneManager.GetActiveScene().name == "InGameScene")
        {
            StoryTeller.Instance.StartCoroutine(Load());
        }
#endif
    }

    static IEnumerator Load()
    {
        yield return new MasterGetterFromServer().GetMaster(
            () => { },
            ParentErrorWindow);
        yield return StoryTeller.Instance.Initialize(StoryGeneratorFromArray.OtherSheedId, () => { }, new GameStartParameter());
    }

    private IStoryGenerator currentStoryGenerator = null;
    private int currentChapterInidex = 0;
    private int startInidex = 0;
    private HitoResources currentHitoResources;

	// 実際はタイトルでタップされた瞬間に呼ばれるようにしたい
    public IEnumerator Initialize(string storyId, Action next, GameStartParameter pauseParameter)
	{
        // Storyのジェネレーターを選択
        currentStoryGenerator = new StoryGeneratorFromArray(storyId);
        this.storyId = storyId;

        // 読み込みを非同期で行う
        yield return currentStoryGenerator.LoadAsync(ParentErrorWindow);

        currentChapterInidex = pauseParameter.StartChapter;
        startInidex = pauseParameter.StartIndex;
        currentHitoResources = pauseParameter.hitoResources;

        // 次の動作を行う
        next();
	}

    public HitoResources GetCurrentHitoResources()
    {
        return currentHitoResources;
    }

    public Chapter GetCurrentChapter()
    {
        return currentStoryGenerator.GetChapter(currentChapterInidex);
    }

    public int GetCurrentChapterIndex()
    {
        return currentChapterInidex;
    }

    public int GetStartInidex()
    {
        return startInidex;
    }

    public string GetCurrentBackgroundPath()
    {
        return currentStoryGenerator.GetChapterBackground(currentChapterInidex);
    }

    public void ProgressChapter()
    {
        startInidex = 0;
        currentChapterInidex++;
    }

    public void GoToNextChapter()
    {
        if (IsFinishStory()) {

            // リザルトに移動する
            SceneManager.LoadScene("ResultScene");

        }
        else {
            // シーンを再ロード
            SceneManager.LoadScene("InGameScene");
        }
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private bool IsFinishStory()
    {
        return currentChapterInidex >= currentStoryGenerator.GetChapterCount();
    }
}
