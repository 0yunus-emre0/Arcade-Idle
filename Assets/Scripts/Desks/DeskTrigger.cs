using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTrigger : MonoBehaviour
{
    public bool isDeskTriggered;
    public Action<bool,Player> OnDeskTriggered;
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Player>(out Player player)) SetDeskTrigger(true,player);
        
    }
    private void OnTriggerExit(Collider other) {
        if(other.TryGetComponent<Player>(out Player player)) SetDeskTrigger(false,player);
    }
    void SetDeskTrigger(bool trigger,Player player){
        isDeskTriggered = trigger;
        OnDeskTriggered?.Invoke(trigger,player);
    }
}
