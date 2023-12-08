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
    public Action<int> OnMiniGameFinished;
    public Action<int> OnGameCoinChanged;
    public Action<int,int> OnCustomerCountChanged;
    #endregion

    #region GameVariables
    public int GameCoin{get;private set;}
    public int customerCount = 1;
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
    public void SetGameCoin(int amount){
        GameCoin += amount;
        OnGameCoinChanged?.Invoke(GameCoin);
    }
    public void SetCustomerCount(int customer,int orderIndex){
        customerCount += customer;
        OnCustomerCountChanged?.Invoke(customer,orderIndex);
    }
    public void InvokeMiniGame(QuizType type){
        OnMiniGameInvoked?.Invoke(type);
    }
    public void FinishMiniGame(int score){
        OnMiniGameFinished?.Invoke(score);
        Invoke(nameof(TurnGamePlay),.5f);
    }
    private void TurnGamePlay()=> SetGameState(GameStates.GamePlay);
        
    
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
