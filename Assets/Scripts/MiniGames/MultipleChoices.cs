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
    [SerializeField] GameObject[] logos;

    #region PrivateVariables
    private QuestionsData[] _selectedQuestions = new QuestionsData[3];
    private int _activeQuestionIndex;
    private int _quizScore;
    #endregion

    public Action<bool> OnAnsveredQuestion;
    
    private void Awake() {
        GameManager.Instance.OnMiniGameInvoked += OnMiniGameInvoked;
    }
    private void Start() {
        

    }
    void OnMiniGameInvoked(QuizType type){
        quizType = type;
        for(int i = 0; i < logos.Length; i++){
            if(logos[i].activeSelf)logos[i].SetActive(false);
        }
        logos[type.packIndex].SetActive(true);
        InitQuestion();
    }

    void InitQuestion(){
        quizList = DataService.LoadData<QuizList>(quizType.JsonDataName,quizList);
        System.Random random = new System.Random();
        _selectedQuestions = quizList.questions.OrderBy(x => random.Next()).Take(3).ToArray();
        
        InitTexts();  
    }
    void InitTexts(){
        if(_activeQuestionIndex >= _selectedQuestions.Length) return;
        questionText.text = _selectedQuestions[_activeQuestionIndex].question;
        for(int i = 0; i < _selectedQuestions[_activeQuestionIndex].choices.Length;i++){
            choicesTexts[i].text = _selectedQuestions[_activeQuestionIndex].choices[i];
        }
    }
    public void ChoiceButton (int choiceIndex){
        if(_activeQuestionIndex < _selectedQuestions.Length){
            if(choiceIndex == _selectedQuestions[_activeQuestionIndex].answerIndex){
                Debug.Log("Answer is Correct");
                _quizScore ++;
                OnAnsveredQuestion?.Invoke(true);
            }
            else{
                Debug.Log("Answer is Wrong");
                OnAnsveredQuestion?.Invoke(false);
            }
            
            _activeQuestionIndex++;
            InitTexts();
            if(_activeQuestionIndex >= _selectedQuestions.Length){
                GameManager.Instance.FinishMiniGame(_quizScore);
                ResetQuiz();
            }
        }
    }
    void ResetQuiz(){
        _activeQuestionIndex = 0;
        _quizScore = 0;
    }

    private void OnDestroy() {
        GameManager.Instance.OnMiniGameInvoked -= OnMiniGameInvoked;
    }

    
}
