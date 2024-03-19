using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    private PlayerMovement _playerMovement; // PlayerMovement ��ũ��Ʈ ����
    private PlayerAttacks _playerAttacks;
    private Vector2 _inputVector; // �÷��̾��� ������ �Է��� �����ϴ� ����
    Dictionary<Paction, bool> lockAction = new Dictionary<Paction, bool>(); 
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAttacks = GetComponent<PlayerAttacks>();
        InItLockAction();
    }

    void FixedUpdate()
    {
        _playerMovement.Move(_inputVector.x);

    }
    void InItLockAction()
    {
        lockAction.Add(Paction.AirAttack,false);
        lockAction.Add(Paction.ChargeShot,false);
        lockAction.Add(Paction.Dash,false);
        lockAction.Add(Paction.DubleJump,false);
        lockAction.Add(Paction.MeleeAttack,false);
        lockAction.Add(Paction.RangeAttack,false);
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
        if (context.performed && lockAction[Paction.Dash]) // ��� ��ư�� ������ ����
        {
            _playerMovement.Dash();
        }
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed && lockAction[Paction.RangeAttack]) // �߻� ��ư�� ������ ����
        {
            _playerAttacks.Fire();
        }
    }

    public void OnAttack(InputAction.CallbackContext context) // ���� ���� �ӽ�
    {
        if (context.performed && lockAction[Paction.MeleeAttack]) 
        {
            _playerAttacks.Attack();
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
    public void UnLockAction(Paction unLockAction)
    {
        lockAction[unLockAction] = true;
    }
}
