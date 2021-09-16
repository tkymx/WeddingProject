using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour {

    [SerializeField]
    Button otherStartButton = null;

    [SerializeField]
    Button takuStartButton = null;

    [SerializeField]
    Button higuStartButton = null;

    [SerializeField]
    Button reloadPauseButton = null;

    [SerializeField]
    Text scoreText = null;

    [SerializeField]
    Button zukanButton = null;

    [SerializeField]
    ZukanPresenter zukanPresenter = null;

    [SerializeField]
    PasswordView passwordView = null;

    [SerializeField]
    GameObject prentErrorWindow = null;

    private void ShowAndCloseButtonIfNessesary(bool isLock)
    {
        otherStartButton.gameObject.SetActive(!isLock);
        takuStartButton.gameObject.SetActive(isLock);
        higuStartButton.gameObject.SetActive(isLock);
        zukanButton.gameObject.SetActive(isLock);
    }

    private void UpdateButtonIfNessesary()
    {
        // 中断が会った場合は中断から
        reloadPauseButton.gameObject.SetActive(PauseReLoader.IsPauseReload());
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

	// Use this for initialization
    void Start () {

        zukanPresenter.gameObject.SetActive(false);
        passwordView.gameObject.SetActive(false);

        var systemModel = SystemRepository.Get();
        if (systemModel == null || !systemModel.IsInitializeDone) {

            // タッチガードを付ける
            var loadingMasterPresenter = LoadingTouchGuardDialogGenerator.Generate(gameObject, LoadingTouchGuardDialogGenerator.Type.Balck);

            // マスターデータの読み込み
            var masterGetter = new MasterGetterFromServer();
            StartCoroutine(masterGetter.GetMaster(() => {

                // タッチガードを取り消す
                loadingMasterPresenter.Close();

                //もしロックされていたらパスワード画面へ
                systemModel = SystemRepository.Get();
                if (systemModel.IsLock)
                {
                    passwordView.gameObject.SetActive(true);
                    passwordView.Initialize(() =>
                    {
                        passwordView.gameObject.SetActive(false);
                        systemModel.OK();
                    });
                }

                // 表示非表示を切り替える
                ShowAndCloseButtonIfNessesary(systemModel.IsLock);
            },
            prentErrorWindow));
        }

        otherStartButton.onClick.AddListener(() =>
        {
            PointStore.Instance.InitializeCurrentGamePoint();
            var loadingPresenter = LoadingTouchGuardDialogGenerator.Generate(gameObject);

            StartCoroutine(StoryTeller.Instance.Initialize(StoryGeneratorFromArray.OtherSheedId, () => {
                StoryTeller.Instance.GoToNextChapter();
                loadingPresenter.Close();
            }, new StoryTeller.GameStartParameter()));
        });

        takuStartButton.onClick.AddListener(() =>
        {
            PointStore.Instance.InitializeCurrentGamePoint();
            var loadingPresenter = LoadingTouchGuardDialogGenerator.Generate(gameObject);

            StartCoroutine(StoryTeller.Instance.Initialize(StoryGeneratorFromArray.TakuSheedId, () =>
            {
                StoryTeller.Instance.GoToNextChapter();
                loadingPresenter.Close();
            }, new StoryTeller.GameStartParameter()
            {
                hitoResources = new HitoResources("Hito/Man")
            }));
        });

        higuStartButton.onClick.AddListener(() =>
        {
            PointStore.Instance.InitializeCurrentGamePoint();
            var loadingPresenter = LoadingTouchGuardDialogGenerator.Generate(gameObject);

            StartCoroutine(StoryTeller.Instance.Initialize(StoryGeneratorFromArray.HiguSheedId, () =>
            {
                StoryTeller.Instance.GoToNextChapter();
                loadingPresenter.Close();
            }, new StoryTeller.GameStartParameter()
            {
                hitoResources = new HitoResources("Hito/Women")
            }));
        });

        reloadPauseButton.onClick.AddListener(() =>
        {
            PointStore.Instance.InitializePauseGamePoint(PauseReLoader.ReloadPoint);
            var loadingPresenter = LoadingTouchGuardDialogGenerator.Generate(gameObject);

            StartCoroutine(StoryTeller.Instance.Initialize(PauseReLoader.ReloadStoryId, () =>
            {
                StoryTeller.Instance.GoToNextChapter();
                loadingPresenter.Close();
            }, new StoryTeller.GameStartParameter()
            {
                StartIndex = PauseReLoader.ReloadStartMasuIndex,
                StartChapter = PauseReLoader.ReloadChapter,
                hitoResources = new HitoResources(PauseReLoader.ReloadHitoPath)
            }));

        });

        zukanButton.onClick.AddListener(() =>
        {
            zukanPresenter.gameObject.SetActive(true);
            zukanPresenter.Initialize();
        });

    }
    
	void Update () {
        
        // スコアの設定
        scoreText.text = PointStore.Instance.MaxGamePoint.ToString() + "pt";

        // ボタン関連
        UpdateButtonIfNessesary();
	}
}
