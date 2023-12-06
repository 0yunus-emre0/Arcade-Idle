using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolGenerator : MonoBehaviour
{
    [SerializeField] CustomerGenerator customerGenerator;
    [SerializeField] OrderDesk orderDesk;
    [SerializeField] PoolBase[] poolBase;
    
   
    public GameObject GetFromPoolBase(int objectTypeIndex,int poolIndex){
        GameObject obj = poolBase[objectTypeIndex].pools[poolIndex].pooledObjects.Dequeue();
        obj.SetActive(true);
        poolBase[objectTypeIndex].pools[poolIndex].pooledObjects.Enqueue(obj);
        return obj;
    }
    void Awake() {
        GeneratePoolBase();
        
    }
    void InitObjects(GameObject poolObject,GameObject poolBase){
        if(poolObject.TryGetComponent<CoursePackage>(out CoursePackage coursePackage)){
            coursePackage.Init(this,poolBase);
        }
        else if(poolObject.TryGetComponent<Customer>(out Customer customer)){
            customer.Init(customerGenerator,this,orderDesk);
        }
    }
    
    void GeneratePoolBase(){
        GameObject gameObjectPool = new GameObject("Pool Base");
        for(int a = 0; a < poolBase.Length; a++){
            GameObject poolTypes = new GameObject(poolBase[a].poolType);
            poolTypes.transform.SetParent(gameObjectPool.transform);
            for(int b = 0; b < poolBase[a].pools.Length; b++){
                GameObject pooledGameObjects = new GameObject(poolBase[a].pools[b].poolName);
                pooledGameObjects.transform.SetParent(poolTypes.transform);
                poolBase[a].pools[b].pooledObjects = new Queue<GameObject>();
                for(int c = 0; c < poolBase[a].pools[b].poolSize; c++){
                    GameObject obj = Instantiate(poolBase[a].pools[b].objectPrefab);
                    obj.name = poolBase[a].pools[b].poolName + " "+ (c+1);
                    obj.transform.SetParent(pooledGameObjects.transform);
                    InitObjects(obj,pooledGameObjects);
                    obj.SetActive(false);
                    poolBase[a].pools[b].pooledObjects.Enqueue(obj);
                }
            }
        }
    }
    
}
[Serializable]
public struct Pool{
    public Queue<GameObject> pooledObjects;
    public string poolName;
    public GameObject objectPrefab;
    public int poolSize;
        

}
[Serializable]
public struct PoolBase{
    public string poolType;
    public Pool[] pools;

}