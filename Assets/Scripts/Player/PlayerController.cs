using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInputAction.IPlayerActions
{
    private PlayerInputAction inputAction;
    private PlayerMovement movement;
    private Vector2 inputVector; // �Է� ����� ����


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
        // �Է� ���͸� �о�ͼ� PlayerMovement�� ����
        inputVector = context.ReadValue<Vector2>(); // nomalized
    }

    private void FixedUpdate()
    {
        // inputVector�� ����Ͽ� Move �޼��� ȣ��
        if (movement != null)
        {
            movement.Move(inputVector); // �Է��� PlayerMovement.cs�� Move�� ����
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.performed) // ���� ��ư�� ������ ����
        {
            Debug.Log("����!");
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
