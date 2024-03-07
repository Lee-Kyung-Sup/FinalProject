using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInputAction.IPlayerActions
{
    private PlayerInputAction inputAction;
    private PlayerMovement movement;
    private Vector2 inputVector; // 입력 저장용 변수


    void Awake()
    {
        movement = GetComponent<PlayerMovement>();

    }

    private void OnEnable()
    {
        if (inputAction == null)
            inputAction = new PlayerInputAction();

        inputAction.Player.SetCallbacks(instance: this);
        inputAction.Player.Enable();
    }

    private void OnDisable()
    {
        inputAction.Player.Disable();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        // 입력 벡터를 읽어와서 PlayerMovement로 전달
        inputVector = context.ReadValue<Vector2>(); // nomalized
    }

    private void FixedUpdate()
    {
        // inputVector를 사용하여 Move 메서드 호출
        if (movement != null)
        {
            movement.Move(inputVector); // 입력을 PlayerMovement.cs의 Move에 전달
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.performed) // 점프 버튼이 눌렸을 때만
        {
            Debug.Log("점프!");
            movement.Jump();
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

    public void OnLook(InputAction.CallbackContext context)
    {

    }


    public void OnInventory(InputAction.CallbackContext context)
    {

    }


}
