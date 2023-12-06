using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    [Header ("References: ")]
    [SerializeField] PoolGenerator _poolGenerator;
    [Header ("Variables: ")]
    public Transform[] spawnPoints;
    public int customerCount;

    private void Awake() {
        customerCount = GameManager.Instance.customerCount;
    }
    private void Start() {
        for(int i = 0; i < customerCount;i++){
            AddCustomer();
        }
    }
    public Transform GetDestroyDestination(){
        int randomIndex = Random.Range(0,spawnPoints.Length);
        return spawnPoints[randomIndex];
    }
    public void AddCustomer(){
        //pool customer
        var customer = _poolGenerator.GetFromPoolBase(PoolIndexs.CustomerBase,PoolIndexs.Customer);
        customer.transform.position = GetDestroyDestination().position;
        if(customer.TryGetComponent<Customer>(out Customer customerComponent)){
            customerComponent.InitCustomerLook();
            customerComponent.GoToOrderDesk();
        }
    }
}
