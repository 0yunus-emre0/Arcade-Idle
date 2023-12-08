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
    List<int> avaliableOrdersList = new List<int>();

    private void Awake() {
        customerCount = GameManager.Instance.customerCount;
        GameManager.Instance.OnCustomerCountChanged += UpdateCustomersAndOrders;
        avaliableOrdersList.Add(0);
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
            int orderIndex = Random.Range(0,avaliableOrdersList.Count);
            int order = avaliableOrdersList[orderIndex];
            customerComponent.GoToOrderDesk(order);
        }
    }
    public void UpdateCustomersAndOrders(int customer,int order){
        avaliableOrdersList.Add(order);
        for(int i = 0; i < customer;i++){
            AddCustomer();
        }
        customerCount += customer;
    }

    private void OnDestroy() {
        GameManager.Instance.OnCustomerCountChanged -= UpdateCustomersAndOrders;
    }
}
