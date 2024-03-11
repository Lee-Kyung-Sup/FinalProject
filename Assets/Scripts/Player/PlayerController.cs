using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    private PlayerMovement _playerMovement; // PlayerMovement 스크립트 참조
    private Vector2 _inputVector; // 플레이어의 움직임 입력을 저장하는 벡터

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        _playerMovement.Move(_inputVector.x);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // 입력 벡터를 업데이트
        float inputX = context.ReadValue<Vector2>().x;
        _inputVector = new Vector2(inputX, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) // 점프 버튼이 눌렸을 때만
        {
            Debug.Log("점프!");
            _playerMovement.Jump();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed) // 점프 버튼이 눌렸을 때만
        {
            Debug.Log("대쉬!");
            _playerMovement.Dash();
        }
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        
    }


    public void OnInteraction(InputAction.CallbackContext context)
    {

    }





    public void OnInventory(InputAction.CallbackContext context)
    {

    }


}
