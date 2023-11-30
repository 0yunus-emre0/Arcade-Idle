using UnityEngine;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
    protected virtual void Awake(){
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    
    protected virtual void OnGameStateChanged(GameStates states){
        
    }

    protected virtual void OnDestroy() {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
}
