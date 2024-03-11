using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    private PlayerMovement _playerMovement; // PlayerMovement ��ũ��Ʈ ����
    private Vector2 _inputVector; // �÷��̾��� ������ �Է��� �����ϴ� ����

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
        // �Է� ���͸� ������Ʈ
        float inputX = context.ReadValue<Vector2>().x;
        _inputVector = new Vector2(inputX, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) // ���� ��ư�� ������ ����
        {
            Debug.Log("����!");
            _playerMovement.Jump();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed) // ���� ��ư�� ������ ����
        {
            Debug.Log("�뽬!");
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
