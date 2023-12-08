using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UIBase
{
    [SerializeField] CanvasGroup pausePanel;
    [SerializeField] CanvasGroup pauseMainPanel;
    [SerializeField] CanvasGroup pauseSettingsPanel;
    [SerializeField] Joystick joystickMove;
    [SerializeField] Animator monitorAnimator;
    [SerializeField] MultipleChoices multipleChoices;
    [SerializeField] Animator successTextAnimator;
    [SerializeField] Image successTextImage;
    [SerializeField] Sprite correctText,wrongText;
    [SerializeField] TextMeshProUGUI gameCoinText;

    protected override void Awake()
    {
        base.Awake();
        multipleChoices.OnAnsveredQuestion += OnAnsveredQuestion;
        GameManager.Instance.OnMiniGameFinished += OnMiniGameFinished;
    }

    protected override void OnGameStateChanged(GameStates states)
    {
        switch(states){
            case GameStates.GamePlay:
                joystickMove.gameObject.SetActive(true);
                break;
            case GameStates.Paused:
                pausePanel.DOFade(1,1f);
                joystickMove.gameObject.SetActive(false);
                break;
            case GameStates.MiniGame:
                monitorAnimator.Play("MonitorFadeIn");
                break;
            default:
                return;
        }
    }
    protected override void OnGameCoinChanged(int gameCoin)
    {
        gameCoinText.text = gameCoin.ToString();
    }
    void OnMiniGameFinished(int score){
        monitorAnimator.Play("MonitorFadeOut");
    }
    void OnAnsveredQuestion(bool answerState){
        successTextImage.sprite = (answerState)? correctText:wrongText;
        successTextAnimator.Play("SuccessTextFadeIn");
    }
    public void OnPauseButtonPressed(){
        if(GameManager.Instance.gameState == GameStates.MiniGame) return;
        pausePanel.blocksRaycasts = true;
        GameManager.Instance.SetGameState(GameStates.Paused);
    }
    public void OnResumeButtonPressed(){
        pausePanel.blocksRaycasts = false;
        pausePanel.DOFade(0,1f);
        GameManager.Instance.SetGameState(GameStates.GamePlay);
    }
    public void OnSettingsButtonPressed(){
        pauseMainPanel.blocksRaycasts = false;
        pauseSettingsPanel.blocksRaycasts = true;
        pauseMainPanel.DOFade(0,.5f);
        pauseSettingsPanel.DOFade(1,.5f);
    }
    public void OnSettingsBackButtonPressed(){
        pauseMainPanel.blocksRaycasts = true;
        pauseSettingsPanel.blocksRaycasts = false;
        pauseMainPanel.DOFade(1,.5f);
        pauseSettingsPanel.DOFade(0,.5f);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        multipleChoices.OnAnsveredQuestion -= OnAnsveredQuestion;
        GameManager.Instance.OnMiniGameFinished += OnMiniGameFinished;
    }
}
