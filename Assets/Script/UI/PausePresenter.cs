using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePresenter : MonoBehaviour {

    [SerializeField]
    private Button pauseButton = null;

    private HitoMasuMover hitoMasuMover;
    public void SetHitoMasuMover(HitoMasuMover hitoMasuMover) 
    {
        this.hitoMasuMover = hitoMasuMover;
    }

	// Use this for initialization
	void Start () {
        pauseButton.onClick.AddListener(() =>
        {
            // ここで情報のセーブを行いたい
            PauseReLoader.EnablePauseReload(true);
            PauseReLoader.ReloadStartMasuIndex = hitoMasuMover.TotalMasuCount;
            PauseReLoader.ReloadPoint = PointStore.Instance.CurrentGamePoint;
            PauseReLoader.ReloadStoryId = StoryTeller.Instance.StoryId;
            PauseReLoader.ReloadChapter = StoryTeller.Instance.GetCurrentChapterIndex();
            PauseReLoader.ReloadHitoPath = StoryTeller.Instance.GetCurrentHitoResources().Path;

            StoryTeller.Instance.GoToTitle();
        });
	}

    public void SetInteractable(bool interactable)
    {
        pauseButton.interactable = interactable;
    }
}
