using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestionsData
{
    public string question;
    public string[] choices;
    public int answerIndex;
}
[System.Serializable]
public class QuizList{
    public QuestionsData[] questions = new QuestionsData[20];
    
}
