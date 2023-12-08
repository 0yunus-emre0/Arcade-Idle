using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderDesk : Desk
{
    [SerializeField] PoolGenerator _poolGenerator;
    [SerializeField] Transform[] _pickPositions;
    [SerializeField] Transform[] _customerDurations;
    //List<int> _emptyPickPositionsIndexs;
    [SerializeField] List<int> _emptyCustomerDurationsIndexs;
    [SerializeField] List<CustomerInfo> _customersOnDesk;
    protected override void Awake()
    {
        base.Awake();
    }
    void Start(){
        
    }
    protected override void OnDeskTriggered(bool trigger, Player player)
    {
        if(player.isPlayerHoldingSomething && _customersOnDesk.Count != 0){
            foreach(CustomerInfo info in _customersOnDesk){
                if(info.orderIndex == player.CoursePackPoolIndex){
                    player.RemovePack();
                    break;
                }
            }

        }
    }
    public void GiveOrder(int orderIndex,int starCount){
        foreach(CustomerInfo info in _customersOnDesk){
            if(info.orderIndex == orderIndex){
                var order = _poolGenerator.GetFromPoolBase(PoolIndexs.CoursePackage,orderIndex);
                order.transform.SetParent(_pickPositions[info.durationIndex]);
                order.transform.localPosition = Vector3.zero;
                if(order.TryGetComponent<CoursePackage>(out CoursePackage coursePackage)){
                    coursePackage.StarCount = starCount;
                    info.customer.ConsumeOrder(coursePackage,orderIndex);
                }
                
                break;
            }
        }
    }
    public void PlugCustomer(CustomerInfo customerInfo,bool isCustomerIn){
        if(isCustomerIn) _customersOnDesk.Add(customerInfo);
        else {
            _emptyCustomerDurationsIndexs.Add(customerInfo.durationIndex);
            _customersOnDesk.Remove(customerInfo);
        }
    }
    public Transform GiveEmptySpaceForCustomer(ref CustomerInfo customerInfo){
        
        int randomTransform = Random.Range(0,_emptyCustomerDurationsIndexs.Count);
        customerInfo.durationIndex = _emptyCustomerDurationsIndexs[randomTransform];
        _emptyCustomerDurationsIndexs.Remove(customerInfo.durationIndex);
        return _customerDurations[customerInfo.durationIndex];
        
    }
    public Vector3 LookAtPosition(CustomerInfo info){
        return _pickPositions[info.durationIndex].position;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
[System.Serializable]
public struct CustomerInfo{
    public int durationIndex;
    public int orderIndex;
    public Customer customer;
}
