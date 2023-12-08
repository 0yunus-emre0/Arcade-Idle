using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using DG.Tweening;
using TMPro;

public class Customer : MonoBehaviour
{
    [Header("Refferances: ")]
    [SerializeField] CustomerGenerator _customerGenerator;
    [SerializeField] PoolGenerator _poolGenerator;
    [SerializeField] OrderDesk _orderDesk;
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] RectTransform _panels;
    [SerializeField] CanvasGroup _loadingPanel;
    [SerializeField] CanvasGroup _starPanel;
    [SerializeField] Image _starFill;
    [SerializeField] Image _timerFill;
    [SerializeField] CanvasGroup _orderPanel;
    [SerializeField] Image _orderImage;
    [SerializeField] Sprite[] _orderLogos;
    [SerializeField] GameObject[] _hairs;
    [SerializeField] Material[] _hairMaterials;
    [SerializeField] GameObject _torso;
    [SerializeField] Animator _customerAnimator;
    [SerializeField] Animator _goldPanelAnimator;
    [SerializeField] TextMeshProUGUI _gainGoldText;
 
    [Header("Variables: ")]
    CustomerInfo _customerInfo = new CustomerInfo();
    private bool _isGoingDesk,_isGoingOut;
    [SerializeField] float _timerFillAmount;
    [SerializeField] float _starFillAmount{
        get{
            return _starFill.fillAmount;
        }
        set{
            _starFill.fillAmount = value;
        }
    }

    public void Init(CustomerGenerator customerGenerator,PoolGenerator poolGenerator, OrderDesk orderDesk){
        _customerGenerator = customerGenerator;
        _poolGenerator = poolGenerator;
        _orderDesk = orderDesk;
    }
    public void InitCustomerLook(){
        for(int i = 0; i < _hairs.Length;i++){
            if(_hairs[i].activeSelf) _hairs[i].SetActive(false);
        }
        int randomIndex = Random.Range(0,_hairs.Length);
        _hairs[randomIndex].SetActive(true);
        int randomHairMaterialIndex = Random.Range(0,_hairMaterials.Length);
        _hairs[randomIndex].GetComponent<Renderer>().material = _hairMaterials[randomHairMaterialIndex];
        Color color = RandomColor();
        _torso.GetComponent<Renderer>().material.color = color;
    }
    private void Start() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _customerInfo.customer = this;
        _orderImage.sprite = _orderLogos[_customerInfo.orderIndex];
        //GoToOrderDesk();
    }
    private void Update() {
        if(!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && _navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete){
            if(_isGoingDesk) AskOrder();
            else if(_isGoingOut) DestroyCustomer();
            _customerAnimator.Play("CustomerIdle");
        }
    }
    private void LateUpdate() {
        _orderPanel.transform.LookAt(_orderPanel.transform.position + Camera.main.transform.forward);
        //_starPanel.transform.LookAt(_orderPanel.transform.position + Camera.main.transform.right);
        //_loadingPanel.transform.LookAt(_orderPanel.transform.position + Camera.main.transform.right);
    }
    public void GoToOrderDesk(int order){
        if(_navMeshAgent == null) Debug.Log("agent null");
        _customerInfo.orderIndex = order;
        _navMeshAgent.SetDestination(_orderDesk.GiveEmptySpaceForCustomer(ref _customerInfo).position);
        Debug.Log(gameObject.name + " " + _customerInfo.durationIndex);
        _customerAnimator.Play("CustomerWalk");
        _isGoingDesk = true;
    }
    void GoToOut(){
        _customerGenerator.AddCustomer();
        _navMeshAgent.SetDestination(_customerGenerator.GetDestroyDestination().position);
        _customerAnimator.Play("CustomerWalk");
        _isGoingOut = true;
    }
    public void ConsumeOrder(CoursePackage order,int orderIndex){
        
        _orderPanel.DOFade(0,1f);
        _starPanel.DOFade(1,.5f);
        _starFill.fillAmount = 0;
        _loadingPanel.DOFade(1,1f).OnComplete(()=>{            
            _starFill.DOFillAmount(CalculateStaramount(order.StarCount),_timerFillAmount);
            _timerFill.DOFillAmount(0,_timerFillAmount).OnComplete(()=>{
                Debug.Log("Gave " + order.StarCount + " Stars");
                int payment = order.StarCount * order.goldAmount;
                GameManager.Instance.SetGameCoin(payment);
                //add money
                _starPanel.DOFade(0,.5f);
                _orderDesk.PlugCustomer(_customerInfo,false);
                order.DestroyObject();
                _loadingPanel.DOFade(0,1f).OnComplete(()=>_timerFill.fillAmount = 1);
                _gainGoldText.text = "+" + payment.ToString();
                _goldPanelAnimator.Play("CustomerGoldFade");
                Invoke(nameof(GoToOut),1.5f);
            });
        }
        );

    }
    void AskOrder(){
        
        Vector3 position = _orderDesk.LookAtPosition(_customerInfo);
        Vector3 direction = position - transform.position;
        transform.DOLookAt(direction,.8f);
        _orderPanel.DOFade(1,1f);
        _orderDesk.PlugCustomer(_customerInfo,true);
        _isGoingDesk = false;
    }
    Color RandomColor(){
        float r = Random.value;
        float g = Random.value;
        float b = Random.value;
        return new Color(r,g,b);
    }
    float CalculateStaramount(int star){
        switch(star){
            case 1:
                return .333f;
            case 2:
                return .666f;
            case 3:
                return 1f;
            default:
                return 0f;     
        }
    }
    void DestroyCustomer(){
        //_customerGenerator.AddCustomer();
        
        gameObject.SetActive(false);
    }
}
