using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour {

    [SerializeField]
    Button startButton = null;

	// Use this for initialization
	void Start () {

        // ポイントの保存を行う
        PointStore.Instance.StoreCurrentGamePointToMaxGamePoint();

        // 中断情報の消去
        PauseReLoader.EnablePauseReload(false);

        startButton.onClick.AddListener(() =>
        {
            StoryTeller.Instance.GoToTitle();
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
