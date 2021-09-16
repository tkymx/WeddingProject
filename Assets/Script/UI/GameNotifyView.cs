using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNotifyView : MonoBehaviour {

    [SerializeField]
    private GameObject goToNextNotify = null;

    [SerializeField]
    private GameObject gameStartNotify = null;

    [SerializeField]
    private GameObject gameStartNotifyInternal = null;

    [SerializeField]
    private PointIncrementNotify pointIncrementNotify = null;

    [SerializeField]
    private MasuReaction masuReaction = null;

	private void Start()
	{
        goToNextNotify.SetActive(false);
        gameStartNotify.SetActive(false);
	}

	public void ShowGoToNext()
    {
        goToNextNotify.SetActive(true);
    }

    public void ShowGameStartNotify()
    {
        gameStartNotify.SetActive(true);

        var gameStartNotifyButton = gameStartNotifyInternal.GetComponent<Button>();
        gameStartNotifyButton.onClick.AddListener(() =>
        {
            StartCoroutine("CloseGameStartWhenAnimationEnd");
        });        
    }

    private IEnumerator CloseGameStartWhenAnimationEnd()
    {
        // 閉じるステート
        var gameStartNotifyAnimator = gameStartNotifyInternal.GetComponent<Animator>();
        gameStartNotifyAnimator.SetTrigger("CloseGameStartNotify");

        // 1フレーム待つ
        yield return null;

        // アニメーションの終了まで待つ
        while(true) {
            var animationState = gameStartNotifyAnimator.GetCurrentAnimatorStateInfo(0);
            if(animationState.IsName("GameStartCloseNotify")) {
                break;
            }
            yield return null;
        }

        // 消す
        gameStartNotify.SetActive(false);
    }

    public void AppearPointIncrementNotify(int point)
    {
        pointIncrementNotify.SetPoint(point);
        pointIncrementNotify.Appear();
    }

    public bool CanShowMasuReaction(Masu masu) 
    {
        if (masu.MasuType == Masu.Type.Normal) {
            return false;
        }
        return true;
    }

    public void ShowMasuReaction(Masu masu)
    {
        if (masu.MasuType == Masu.Type.AbNormal)
        {
            masuReaction.ShowAbnormalMasuReactoin();
        }
        else if (masu.MasuType == Masu.Type.Event)
        {
            masuReaction.ShowEventMasuReactoin();
        }
    }

    public void CloseMasuReaction()
    {
        masuReaction.CloseRaction();
    }
}
