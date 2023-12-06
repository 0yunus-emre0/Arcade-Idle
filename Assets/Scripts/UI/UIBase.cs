using UnityEngine;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
    protected virtual void Awake(){
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        GameManager.Instance.OnGameCoinChanged += OnGameCoinChanged;
    }
    
    protected virtual void OnGameStateChanged(GameStates states){
        
    }
    protected virtual void OnGameCoinChanged(int gameCoin){

    }

    protected virtual void OnDestroy() {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        GameManager.Instance.OnGameCoinChanged -= OnGameCoinChanged;
    }
}
