using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolGenerator : MonoBehaviour
{
    //[SerializeField] Pool[] pools = null;
    [SerializeField] PoolBase[] poolBase;
    
    /*
    public GameObject GetFromPool(int objectType){
        GameObject obj = pools[objectType].pooledObjects.Dequeue();
        obj.SetActive(true);
        pools[objectType].pooledObjects.Enqueue(obj);
        return obj;
    }
    */
    public GameObject GetFromPoolBase(int objectTypeIndex,int poolIndex){
        GameObject obj = poolBase[objectTypeIndex].pools[poolIndex].pooledObjects.Dequeue();
        obj.SetActive(true);
        poolBase[objectTypeIndex].pools[poolIndex].pooledObjects.Enqueue(obj);
        return obj;
    }
    void Awake() {
        GeneratePoolBase();
        //GeneratePool();
    }
    /*
    void GeneratePool(){
        GameObject gameObjectPool = new GameObject("Object Pools");
        for(int a=0;a<pools.Length;a++){
            GameObject pooledGameObjects = new GameObject(pools[a].poolName);
            pooledGameObjects.transform.SetParent(gameObjectPool.transform);
            pools[a].pooledObjects = new Queue<GameObject>();
            for(int i = 0; i < pools[a].poolSize; i++) {
                GameObject obj = Instantiate(pools[a].objectPrefab);
                obj.name = pools[a].poolName +" "+ (i+1);
                obj.transform.SetParent(pooledGameObjects.transform);
                obj.SetActive(false);
                pools[a].pooledObjects.Enqueue(obj);
            }
        }
    }
    */
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