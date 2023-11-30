using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTrigger : MonoBehaviour
{
    public bool isDeskTriggered;
    public Action<bool> OnDeskTriggered;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) SetDeskTrigger(true);
        
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) SetDeskTrigger(false);
    }
    void SetDeskTrigger(bool trigger){
        isDeskTriggered = trigger;
        OnDeskTriggered?.Invoke(trigger);
    }
}
