using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UIBase
{
    [SerializeField] Joystick joystickMove;
    [SerializeField] Animator monitorAnimator;
    [SerializeField] MultipleChoices multipleChoices;
    [SerializeField] Animator successTextAnimator;
    [SerializeField] Image successTextImage;
    [SerializeField] Sprite correctText,wrongText;

    protected override void Awake()
    {
        base.Awake();
        multipleChoices.OnAnsveredQuestion += OnAnsveredQuestion;
    }

    protected override void OnGameStateChanged(GameStates states)
    {
        switch(states){
            case GameStates.MiniGame:
                monitorAnimator.Play("MonitorFadeIn");
                break;
            default:
                return;
        }
    }
    void OnAnsveredQuestion(bool answerState){
        successTextImage.sprite = (answerState)? correctText:wrongText;
        successTextAnimator.Play("SuccessTextFadeIn");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        multipleChoices.OnAnsveredQuestion -= OnAnsveredQuestion;
    }
}
