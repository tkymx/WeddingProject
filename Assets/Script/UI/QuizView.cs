using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuizView : MonoBehaviour {

    [SerializeField]
    Text Description = null;

    [SerializeField]
    List<Text> Answers = null;

    [SerializeField]
    List<Button> AnswerButtons = null;

    public void SetTextAndAction(string description, List<string> answers, Action<int> action)
    {
        Description.text = description;
        for (int i = 0; i < 4;i++) {
            Answers[i].text = answers[i];

            var answerIndex = i;
            AnswerButtons[i].onClick.RemoveAllListeners();
            AnswerButtons[i].onClick.AddListener(() =>
            {
                action(answerIndex);
            });
        }
    }
}
