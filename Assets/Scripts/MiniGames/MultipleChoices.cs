using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class MultipleChoices : MonoBehaviour
{
    public QuizType quizType;
    QuizList quizList = new QuizList();
    [Header("Text References: ")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI[] choicesTexts;

    #region PrivateVariables
    private QuestionsData[] _selectedQuestions = new QuestionsData[3];
    private int _activeQuestionIndex;
    #endregion

    public Action<bool> OnAnsveredQuestion;
    
    private void Awake() {
        GameManager.Instance.OnMiniGameInvoked += OnMiniGameInvoked;
    }
    private void Start() {
        

    }
    void OnMiniGameInvoked(QuizType type){
        quizType = type;
        InitQuestion();
    }

    void InitQuestion(){
        quizList = DataService.LoadData<QuizList>(quizType.JsonDataName,quizList);
        System.Random random = new System.Random();
        _selectedQuestions = quizList.questions.OrderBy(x => random.Next()).Take(3).ToArray();
        
        InitTexts();  
    }
    void InitTexts(){
        questionText.text = _selectedQuestions[_activeQuestionIndex].question;
        for(int i = 0; i < _selectedQuestions[_activeQuestionIndex].choices.Length;i++){
            choicesTexts[i].text = _selectedQuestions[_activeQuestionIndex].choices[i];
        }
    }
    public void ChoiceButton (int choiceIndex){
        if(choiceIndex == _selectedQuestions[_activeQuestionIndex].answerIndex){
            Debug.Log("Answer is Correct");
            OnAnsveredQuestion?.Invoke(true);
        }
        else{
            Debug.Log("Answer is Wrong");
            OnAnsveredQuestion?.Invoke(false);
        }
        if(_activeQuestionIndex < _selectedQuestions.Length){
            _activeQuestionIndex++;
            InitTexts();
        }
        else{

        }
    }
    void ResetQuiz(){
        _activeQuestionIndex = 0;
        
    }

    private void OnDestroy() {
        GameManager.Instance.OnMiniGameInvoked -= OnMiniGameInvoked;
    }

    
}