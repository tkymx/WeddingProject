using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DiceRoller : MonoBehaviour {

    [SerializeField]
    private Text prepareText = null;

    [SerializeField]
    private Text diceText = null;

    private Button button;
    private Action<int> onRolled = null;

    // ボタンが有効かどうか？
    public bool Enable
    {
        get 
        {
            return button.interactable;
        }
        set
        {
            if (value == true) {
                ShowPrepareText();
            }

            button.interactable = value;
        }
    }

    public void SetRolledAction(Action<int> onRolled)
    {
        this.onRolled = onRolled;
        button.interactable = true;
    }

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        Enable = false;

        // サイコロを振ったらイベントを実行
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            Enable = false;
            if (onRolled != null) {
                DoingDiceAndApply();
                onRolled = null;
            }
        });
	}

    void DoingDiceAndApply() 
    {
        int diceNumber = UnityEngine.Random.Range(1, 6);

        ShowNumber(diceNumber);
        onRolled(diceNumber);
    }

    void ShowNumber(int number)
    {
        this.prepareText.gameObject.SetActive(false);
        this.diceText.gameObject.SetActive(true);

        this.diceText.text = number.ToString();
    }

    void ShowPrepareText()
    {
        this.prepareText.gameObject.SetActive(true);
        this.diceText.gameObject.SetActive(false);
    }
}
