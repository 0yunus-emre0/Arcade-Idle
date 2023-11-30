using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public abstract class Desk : MonoBehaviour
{
    [Header("Refferances: ")]
    [SerializeField] protected DeskTrigger deskTrigger;
    [SerializeField] protected GameObject deskCanvas;
    [SerializeField] protected CanvasGroup canvasPanel;
    

    protected virtual void Awake(){
        deskTrigger.OnDeskTriggered += OnDeskTriggered;
    }
    protected virtual void LateUpdate() {
        deskCanvas.transform.LookAt(deskCanvas.transform.position + Camera.main.transform.forward);
    }
    protected virtual void OnDeskTriggered(bool trigger){
        canvasPanel.alpha = (trigger)? 1:0;
        
    }

    protected virtual void OnDestroy() {
        deskTrigger.OnDeskTriggered -= OnDeskTriggered;
    }
    
}
