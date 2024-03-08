using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInputAction.IPlayerActions

{ // layerInputAction.IPlayerActions �������̽��� ��ӹ޾�
  // Input System�� ���ǵ� �Է� �׼ǵ��� ����

    private PlayerInputAction inputAction; // �Է� �׼ǵ��� ��� �ִ� Ŭ���� �ν��Ͻ�
    
    //private PlayerMovement movement; // �÷��̾� ���� ������ ���� Ŭ����
    private Vector2 inputVector; // �÷��̾��� ������ �Է��� �����ϴ� ����


    void Awake()
    {
        inputAction = new PlayerInputAction();
        inputAction.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        //��ǲ �ý�����
        //Enable() �޼��带 ȣ���Ͽ� �Է��� Ȱ��ȭ�ؾ߸�
        //�ش� �Է¿� ���� �̺�Ʈ�� ������ �� �ִ�.
        inputAction.Player.Enable();
    }

    private void OnDisable()
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ �Ǹ� �Է� �̺�Ʈ�� �� �̻� ���� ���� (������� ����)
        inputAction.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // �Է� ���͸� �о�ͼ� PlayerMovement.c����
        inputVector = context.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        if (inputVector != Vector2.zero)
        {
            SendMessage("Move", inputVector);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.performed) // ���� ��ư�� ������ ����
        {
            Debug.Log("����!");
            SendMessage("Jump"); // �Է��� PlayerMovement.cs�� Jump�� ����
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
