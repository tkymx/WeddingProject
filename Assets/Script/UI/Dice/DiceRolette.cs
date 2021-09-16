using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DiceRolette : MonoBehaviour
{
    [SerializeField]
    Button diceButton = null;

    [SerializeField]
    GameObject diceHai = null;

    [SerializeField]
    GameObject tapNotify = null;

    private Action<int> onRolled;

    // ボタンが有効かどうか？
    public bool Enable
    {
        get
        {
            return diceButton.interactable;
        }
        set
        {
            tapNotify.SetActive(value);
            diceButton.interactable = value;
        }
    }

    public bool IsRolling()
    {
        return currentDiceState != DiceState.Prepare;
    }

    public void SetRolledAction(Action<int> onRolled)
    {
        this.onRolled = onRolled;
        diceButton.interactable = true;
        currentDiceState = DiceState.Prepare;
    }

    // Use this for initialization
    private void Start()
    {
        Enable = false;

        // サイコロを振ったらイベントを実行
        diceButton.onClick.RemoveAllListeners();
        diceButton.onClick.AddListener(() =>
        {
            Enable = false;
            if (onRolled != null)
            {
                DoDice();
            }
        });
    }

    enum DiceState
    {
        Prepare,
        DoDice,
        FirstDicing,
        DecitionDicing,
        Decide
    }

    private DiceState currentDiceState = DiceState.Prepare;

    private void DoDice()
    {
        currentDiceState = DiceState.DoDice;
    }

    private float tempStartTime = 0;
    private float firstDiceTime = 1;
    private float dicedeDiceTime = 1;

    private void Update()
    {
        if (currentDiceState == DiceState.Prepare)
        {

        }
        else if (currentDiceState == DiceState.DoDice)
        {
            currentDiceState = DiceState.FirstDicing;
            tempStartTime = 0;

            firstDiceTime = UnityEngine.Random.Range(0.5f, 1.5f);
            dicedeDiceTime = UnityEngine.Random.Range(0.5f, 2.5f);

        }
        else if (currentDiceState == DiceState.FirstDicing)
        {
            tempStartTime += Time.deltaTime;
            if (tempStartTime > firstDiceTime)
            {
                currentDiceState = DiceState.DecitionDicing;
                tempStartTime = 0;
                return;
            }

            diceHai.transform.Rotate(0, 0, 10);
        }
        else if (currentDiceState == DiceState.DecitionDicing)
        {
            tempStartTime += Time.deltaTime;
            if (tempStartTime > dicedeDiceTime)
            {
                currentDiceState = DiceState.Decide;
                tempStartTime = 0;
                return;
            }

            // 段々と止まるようにした。
            diceHai.transform.Rotate(0, 0, 10 * (1 - tempStartTime / dicedeDiceTime));
        }
        else if (currentDiceState == DiceState.Decide)
        {
            float zangle = getNormalizedDgree((int)diceHai.transform.eulerAngles.z);
            int value = 0;

            if (0 <= zangle && zangle < 60)
            {
                value = 6;
            }
            else if (60 <= zangle && zangle < 120)
            {
                value = 5;
            }
            else if (120 <= zangle && zangle < 180)
            {
                value = 4;
            }
            else if (180 <= zangle && zangle < 240)
            {
                value = 3;
            }
            else if (240 <= zangle && zangle < 300)
            {
                value = 2;
            }
            else if (300 <= zangle && zangle < 360)
            {
                value = 1;
            }

            if (DebugMenu.Instance.DebugParameter.DiceForce) {
                value = DebugMenu.Instance.DebugParameter.DiceForceCount;
            }

            this.onRolled(value);
            this.onRolled = null;

            currentDiceState = DiceState.Prepare;

        }
	}
    private int getNormalizedDgree(int degree)
    {
        var outputDegree = degree % 360;
        if (outputDegree < 0)
        {
            outputDegree += 360;
        }
        return outputDegree;
    }
}
