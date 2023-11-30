using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("References: ")]
    [SerializeField] private Joystick _joystickMove;
    private CharacterController _characterController;

    [Header("Variables:")]
    [SerializeField] private float _speed;
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _smoothVelocity;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
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

        }
    }
    void OnGameStateChanged(GameStates states){
        switch(states){
            case GameStates.MiniGame:
                break;
            default:
                return;
        }
    }

    private void OnDestroy() {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

}
