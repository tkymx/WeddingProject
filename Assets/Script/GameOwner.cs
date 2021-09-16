using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameOwner : MonoBehaviour {

    [SerializeField]
    private GameObject masuGroup = null;

    [SerializeField]
    private Hito hito = null;

    [SerializeField]
    private DiceRolette diceRoller = null;

    [SerializeField]
    private QuizView quizView = null; 

    [SerializeField]
    private QuizReactionView quizReactionView = null;

    [SerializeField]
    private ConversationTextGroupView conversationTextGroupView = null; 

    [SerializeField]
    private GameNotifyView gameNotifyView = null;

    [SerializeField]
    private RepeatedBackgroundView backgroundView = null;

    [SerializeField]
    private PausePresenter pausePresenter = null;

    private QuizOwner quizOwner;
    private HitoMasuMover hitoMasuMover;
    private ConversationTextManager conversationTextManager;

    enum Progress
    {
        Initialize,
        TurnStart,
        Dice,
        Move,
        MoveEnd,
        MasuReaction,
        TextInitialize,
        TextUpdate,
        QuizInitialize,
        QuizUpdate,
        AfterTextInitialize,
        AfterTextUpdate,
        Reaction,
        Finish
    };

    private Progress currentProgress;
    private bool isClearReaction = false;

	// Use this for initialization
	void Start () {

        // 初期化
        quizOwner = new QuizOwner(quizView);
        currentProgress = Progress.Initialize;
        pausePresenter.SetInteractable(false);
	}

	// Update is called once per frame
	void Update () {

        // 順序
        if (currentProgress == Progress.Initialize)
        {
            // マップの初期化
            var currentChapter = StoryTeller.Instance.GetCurrentChapter();
            currentChapter.Initialize(masuGroup);

            // マップの背景の初期化
            backgroundView.Initialize(
                ResourceManager.GetPrefab(StoryTeller.Instance.GetCurrentBackgroundPath()),
                backgroundView.gameObject,
                MasuDefine.GetMasuPositoinY(masuGroup.transform.childCount+10) //適当な長さ
            );

            // 初期化
            var masus = masuGroup.GetComponentsInChildren<Masu>().ToList();
            hitoMasuMover = new HitoMasuMover(hito, MasuSummaryGenerator.Generate(masus, StoryTeller.Instance.GetStartInidex()));

            pausePresenter.SetHitoMasuMover(hitoMasuMover);

            // 人の初期化
            hito.ChangeHitoImage(StoryTeller.Instance.GetCurrentHitoResources());

            // 消しておく
            conversationTextGroupView.gameObject.SetActive(false);

            currentProgress = Progress.TurnStart;
        }
        else if (currentProgress == Progress.TurnStart) {

            // 必要な場合説明文を出す
            var isShowedDescription = PlayerPrefs.GetInt("IsShowedDescription", 0) == 1;
            if (!isShowedDescription && SystemRepository.Get().IsLock) {
                gameNotifyView.ShowGameStartNotify();
                PlayerPrefs.SetInt("IsShowedDescription", 1);
            }

            if (hitoMasuMover.CurrentMasu.IsFinish()) {
                currentProgress = Progress.Finish;
            }
            else {                
                currentProgress = Progress.Dice;
            }
        }
        else if (currentProgress == Progress.Dice) {

            // ポーズボタンを有効にする
            pausePresenter.SetInteractable(true);

            // ダイスを回したら、次に進む
            if (!diceRoller.IsRolling()) {
                diceRoller.Enable = true;
                diceRoller.SetRolledAction((int progress) => {
                    hitoMasuMover.MoveStart(progress);

                    // 次に進む際にポーズボタンを無効にする
                    pausePresenter.SetInteractable(false);
                    currentProgress = Progress.Move;
                });
            }
        }
        else if (currentProgress == Progress.Move)
        {
            diceRoller.Enable = false;

            // 進めるなら進む
            if (hitoMasuMover.IsFinish())
            {
                currentProgress = Progress.MoveEnd;
            }

            hitoMasuMover.Move();
        }
        else if (currentProgress == Progress.MoveEnd) 
        {
            if (hitoMasuMover.CurrentMasu.IsFinish())
            {
                currentProgress = Progress.Finish;
            }
            else {
                // 特定のマスの場合マスの種類の表示を行う
                if (gameNotifyView.CanShowMasuReaction(hitoMasuMover.CurrentMasu)) {
                    gameNotifyView.ShowMasuReaction(hitoMasuMover.CurrentMasu);
                    currentProgress = Progress.MasuReaction;            
                }else {
                    currentProgress = Progress.TextInitialize;
                }
            }
        }
        else if (currentProgress == Progress.MasuReaction)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameNotifyView.CloseMasuReaction();
                currentProgress = Progress.TextInitialize;
            }
        }
        else if (currentProgress == Progress.TextInitialize)
        {
            conversationTextGroupView.gameObject.SetActive(true);
            conversationTextManager = new ConversationTextManager(
                hitoMasuMover.CurrentMasu.ConversationTextGroup,
                conversationTextGroupView
            );
            currentProgress = Progress.TextUpdate;
            conversationTextManager.UpdateView();
        }
        else if (currentProgress == Progress.TextUpdate)
        {
            if (Input.GetMouseButtonDown(0)) {
                if (conversationTextManager.IsNextFinal()) {
                    conversationTextGroupView.gameObject.SetActive(false);
                    currentProgress = Progress.QuizInitialize;
                }
                else {
                    conversationTextManager.NextText();
                }
            }
            conversationTextManager.UpdateView();
        }
        else if (currentProgress == Progress.QuizInitialize)
        {
            quizOwner.Show(hitoMasuMover.CurrentMasu.Quiz, () =>
            {
                currentProgress = Progress.AfterTextInitialize;
                isClearReaction = true;

            }, () =>
            {
                currentProgress = Progress.AfterTextInitialize;
                isClearReaction = false;
            });

            currentProgress = Progress.QuizUpdate;
        }
        else if (currentProgress == Progress.QuizUpdate)
        {
        }
        else if (currentProgress == Progress.AfterTextInitialize)
        {
            conversationTextGroupView.gameObject.SetActive(true);
            conversationTextManager = new ConversationTextManager(
                hitoMasuMover.CurrentMasu.AfterConversationTextGroup,
                conversationTextGroupView
            );
            currentProgress = Progress.AfterTextUpdate;
            conversationTextManager.UpdateView();
        }
        else if (currentProgress == Progress.AfterTextUpdate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (conversationTextManager.IsNextFinal())
                {
                    conversationTextGroupView.gameObject.SetActive(false);

                    // クイズ結果表示
                    quizReactionView.Show(isClearReaction);

                    // ポイントアニメーション
                    if (isClearReaction) {
                        AppearQuizClearNotify();
                    }

                    currentProgress = Progress.Reaction;                
                }
                else
                {
                    conversationTextManager.NextText();
                }
            }
            conversationTextManager.UpdateView();
        }

        else if (currentProgress == Progress.Reaction)
        {
            if (Input.GetMouseButtonDown(0))
            {
                quizReactionView.Close();
                currentProgress = Progress.TurnStart;
            }
        }
        else if (currentProgress == Progress.Finish)
        {
            gameNotifyView.ShowGoToNext();

            if (Input.GetMouseButtonDown(0))
            {
                StoryTeller.Instance.ProgressChapter();
                StoryTeller.Instance.GoToNextChapter();
            }
        }
	}

    private void AppearQuizClearNotify()
    {
        quizOwner.AddClearPoint(hitoMasuMover.CurrentMasu.Quiz);
        gameNotifyView.AppearPointIncrementNotify(hitoMasuMover.CurrentMasu.Quiz.answerPoint);
    }
}
