using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{ // layerInputAction.IPlayerActions 인터페이스를 상속받아
  // Input System에 정의된 입력 액션들을 제어

    private PlayerMovement playerMovement;
    private Vector2 inputVector; // 플레이어의 움직임 입력을 저장하는 벡터

    void Awake()
    {
        //inputAction = new PlayerInputAction();
        //inputAction.Player.SetCallbacks(this);

        playerMovement = GetComponent<PlayerMovement>();
    }

    //private void OnEnable()
    //{
    //    //인풋 시스템은
    //    //Enable() 메서드를 호출하여 입력을 활성화해야만
    //    //해당 입력에 대한 이벤트를 수신할 수 있다.
    //    inputAction.Player.Enable();
    //}

    //private void OnDisable()
    //{
    //    // 스크립트가 비활성화 되면 입력 이벤트를 더 이상 받지 않음 (오버헤드 방지)
    //    inputAction.Player.Disable();
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        // 입력 벡터를 읽어와서 PlayerMovement.c전달
        inputVector = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        playerMovement.Move(inputVector);
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.performed) // 점프 버튼이 눌렸을 때만
        {
            Debug.Log("점프!");
            playerMovement.Jump();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {

    }


    public void OnInteraction(InputAction.CallbackContext context)
    {

    }

    public void OnDash(InputAction.CallbackContext context)
    {

    }



    public void OnInventory(InputAction.CallbackContext context)
    {

    }


}
