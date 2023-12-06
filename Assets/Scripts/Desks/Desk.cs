using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public abstract class Desk : MonoBehaviour
{
    [Header("Refferances: ")]
    [SerializeField] protected DeskTrigger deskTrigger;
    //[SerializeField] protected GameObject deskCanvas;
    //[SerializeField] protected CanvasGroup canvasPanel;
    

    protected virtual void Awake(){
        deskTrigger.OnDeskTriggered += OnDeskTriggered;
    }
    
    protected virtual void OnDeskTriggered(bool trigger,Player player){
        
    }

    protected virtual void OnDestroy() {
        deskTrigger.OnDeskTriggered -= OnDeskTriggered;
    }
    
}
