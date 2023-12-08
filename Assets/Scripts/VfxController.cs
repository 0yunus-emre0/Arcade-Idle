using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxController : MonoBehaviour
{
    [SerializeField] float _destroyTime;

    private void OnEnable() {
        Invoke(nameof(DestroyVfx),_destroyTime);
    }
    void DestroyVfx(){
        gameObject.SetActive(false);
    }
}
