using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("GameManager");
                _instance = singletonObject.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    public GameStates gameState {get;private set;}

    #region Events
    public Action<GameStates> OnGameStateChanged;
    public Action<QuizType> OnMiniGameInvoked;
    #endregion

    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
                
    }
    public void InvokeMiniGame(QuizType type){
        OnMiniGameInvoked?.Invoke(type);
    }
    public void SetGameState (GameStates state){
        if(state == gameState) return;
        gameState = state;
        OnGameStateChanged?.Invoke(gameState);
    }
}
public enum GameStates{
    GamePlay,
    MiniGame,
    Paused
}
