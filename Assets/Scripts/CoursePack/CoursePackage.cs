using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursePackage : MonoBehaviour
{
    private GameObject _poolBase;
    private PoolGenerator _poolGenerator;
    public int StarCount;
    public int goldAmount;
    public void Init(PoolGenerator poolGenerator,GameObject baseGameObject){
        _poolGenerator = poolGenerator;
        _poolBase = baseGameObject;
    }
    

    public void DestroyObject(){
        transform.SetParent(_poolBase.transform);
        StarCount = 0;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
