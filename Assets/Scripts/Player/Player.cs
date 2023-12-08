using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    [Header("References: ")]
    [SerializeField] private PoolGenerator _poolGenerator;
    [SerializeField] private Joystick _joystickMove;
    [SerializeField] private Transform _holdingPoint;
    [SerializeField] private CanvasGroup _loadingPanel;
    [SerializeField] private Image _timerFill;
    [SerializeField] OrderDesk _orderDesk;
    [SerializeField] private Animator _playerAnimator;
    private CharacterController _characterController;

    [Header("Variables:")]
    [SerializeField] private float _speed;
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _smoothVelocity;
    [SerializeField] private float _timerFillAmount;
    private GameObject pack;
    private int packScore;

    public bool isPlayerHoldingSomething;
    public int CoursePackPoolIndex{get;private set;}
    
    

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        GameManager.Instance.OnMiniGameFinished += OnMiniGameFinished;

    }
    private void Update() {
        Move();
    }
    
    void Move(){
        if(_joystickMove.Direction.magnitude >= .1f){
            Vector3 direction = new Vector3(_joystickMove.Horizontal,0f,_joystickMove.Vertical).normalized;
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref _smoothVelocity, _smoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            _characterController.Move(direction * _speed * Time.deltaTime /*+ gravitydirection*/);
            //anim code
            AnimationsHandler(true);
        }
        else AnimationsHandler(false);
        
    }
    void OnGameStateChanged(GameStates states){
        switch(states){
            case GameStates.MiniGame:
                break;
            default:
                return;
        }
    }
    void OnMiniGameFinished(int score){
        if(score == 0) Debug.Log("Course fail");
        else{
            packScore = score;
            _loadingPanel.DOFade(1,.3f).OnComplete(()=>
            _timerFill.DOFillAmount(0,_timerFillAmount).OnComplete(SpawnPack)
            );            
        }
    }
    void SpawnPack(){
        _loadingPanel.alpha = 0;
        _timerFill.fillAmount = 1;
        //pool pack
        pack = _poolGenerator.GetFromPoolBase(PoolIndexs.CoursePackage,CoursePackPoolIndex);
        pack.transform.SetParent(_holdingPoint);
        pack.transform.localPosition = Vector3.zero;
        pack.transform.localEulerAngles = Vector3.zero;
        pack.GetComponent<CoursePackage>().StarCount = packScore;
        isPlayerHoldingSomething = true;
    }
    public void InitPack(int packIndex) => CoursePackPoolIndex = packIndex;

    public void RemovePack(){
        _loadingPanel.DOFade(1,.3f).OnComplete(()=>
        _timerFill.DOFillAmount(0,_timerFillAmount).OnComplete(()=>{
            pack.GetComponent<CoursePackage>().DestroyObject();
            _orderDesk.GiveOrder(CoursePackPoolIndex,packScore);
            _loadingPanel.alpha = 0;
            _timerFill.fillAmount = 1;
            isPlayerHoldingSomething = false;
        })
        );
        
    }
    void AnimationsHandler(bool isMoving){
        
        if(_playerAnimator.GetBool("isMoving") == isMoving) return;
        _playerAnimator.SetBool("isMoving",isMoving);
        
    }
    

    private void OnDestroy() {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        GameManager.Instance.OnMiniGameFinished += OnMiniGameFinished;

    }

}
