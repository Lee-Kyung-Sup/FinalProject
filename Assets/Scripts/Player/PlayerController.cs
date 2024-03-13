using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    private PlayerMovement _playerMovement; // PlayerMovement ��ũ��Ʈ ����
    private PlayerAttacks _playerAttacks;
    private Vector2 _inputVector; // �÷��̾��� ������ �Է��� �����ϴ� ����



    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAttacks = GetComponent<PlayerAttacks>();
    }

    void FixedUpdate()
    {
        _playerMovement.Move(_inputVector.x);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // �Է� ���͸� ������Ʈ
        float inputX = context.ReadValue<Vector2>().x;
        _inputVector = new Vector2(inputX, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerMovement.Jump();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed) // ��� ��ư�� ������ ����
        {
            _playerMovement.Dash();
        }
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) // �߻� ��ư�� ������ ����
        {
            _playerAttacks.Fire();
        }
    }


    public void OnInteraction(InputAction.CallbackContext context)
    {

    }

    public void OnInventory(InputAction.CallbackContext context)
    {

    }

    public void OnDown(InputAction.CallbackContext context)
    {
        bool isPressing = context.ReadValue<float>() > 0;
        _playerMovement.SetIsPressingDown(isPressing);
    }

}
