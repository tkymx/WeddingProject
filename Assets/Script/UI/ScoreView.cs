using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {

    public Text scoreText = null;
    public bool isMaxValue = false;
    public bool isBeforeValue = false;

	// Update is called once per frame
	void Update () {
        if (isMaxValue) {
            scoreText.text = PointStore.Instance.MaxGamePoint.ToString() + "pt";
        }
        else if (isBeforeValue) {
            scoreText.text = PointStore.Instance.BeforeIncrementScore.ToString() + "pt";
        }
        else {
            scoreText.text = PointStore.Instance.CurrentGamePoint.ToString() + "pt";
        }
	}
}
