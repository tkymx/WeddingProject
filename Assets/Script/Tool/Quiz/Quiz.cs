using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Quiz
{
    public string Destruction;
    public List<string> Answers;
    public int answerIndex;
    public int answerPoint;

    static public Quiz SetValue(string description, string ans1, string ans2, string ans3, string ans4, int answerIndex, int answerPoint)
    {
        Quiz quiz = new Quiz();
        quiz.Destruction = description;
        quiz.Answers = new List<string>(4){
            ans1,ans2,ans3,ans4
        };
        quiz.answerIndex = answerIndex; 
        quiz.answerPoint = answerPoint;

        return quiz;
    }

    public bool IsClear(int index) 
    {
        return index == answerIndex - 1;  //プログラムは0から始まるため
    }
}
