using UnityEngine;
using System;

public static class DataService
{
    public static T LoadData<T>(string jsonData,T Data){
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonData);
        try{
            T data = JsonUtility.FromJson<T>(jsonTextAsset.ToString());
            //Debug.Log("Added Info");
            return data;
        }
        catch(Exception e){
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }   
        
    }
}
