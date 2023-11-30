using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityDesk : Desk
{
    [SerializeField] private QuizType _quizType;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public void OnPrepareButtonPressed(){
        GameManager.Instance.InvokeMiniGame(_quizType);
        GameManager.Instance.SetGameState(GameStates.MiniGame);
    }

    protected override void OnDeskTriggered(bool trigger)
    {
        base.OnDeskTriggered(trigger);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
