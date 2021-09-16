using UnityEngine;
using System.Collections;
using System;

public class QuizOwner
{
    private QuizView quizView;

    public QuizOwner(QuizView quizView) 
    {
        this.quizView = quizView;
        quizView.gameObject.SetActive(false);
    }

    public void Show(Quiz quiz, Action onClear, Action onFailed) 
    {
        quizView.gameObject.SetActive(true);
        quizView.SetTextAndAction(
            quiz.Destruction,
            quiz.Answers,
            (int answeredIndex) =>
            {
                if (quiz.IsClear(answeredIndex))
                {
                    onClear();
                }
                else {
                    onFailed();
                }
                quizView.gameObject.SetActive(false);
            });
    }

    public void AddClearPoint(Quiz quiz)
    {
        PointStore.Instance.AddPoint(quiz.answerPoint);
    }
}
