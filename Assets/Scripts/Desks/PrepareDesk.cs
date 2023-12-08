using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PrepareDesk : Desk
{
    public QuizType _quizType;
    [SerializeField] private GameObject deskCanvas;
    [SerializeField] private CanvasGroup canvasPanel;
    


    protected override void Awake()
    {
        base.Awake();
    }

    void LateUpdate()
    {
        deskCanvas.transform.LookAt(deskCanvas.transform.position + Camera.main.transform.forward);
    }

    public void OnPrepareButtonPressed(){
        GameManager.Instance.InvokeMiniGame(_quizType);
        GameManager.Instance.SetGameState(GameStates.MiniGame);
        canvasPanel.DOFade(0,.5f);
    }

    protected override void OnDeskTriggered(bool trigger,Player player)
    {
        base.OnDeskTriggered(trigger,player);
        if(!player.isPlayerHoldingSomething) canvasPanel.alpha = (trigger)? 1:0; 
        if(trigger) player.InitPack(_quizType.packIndex);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
