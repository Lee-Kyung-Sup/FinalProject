using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    private PlayerMovement _playerMovement;
    private PlayerAttacks _playerAttacks;

    private Vector2 _inputVector; 
    private bool _isWeaponChange = true; // true �ٰŸ�, false ���Ÿ� ����

    Dictionary<Paction, bool> lockAction = new Dictionary<Paction, bool>();


    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAttacks = GetComponent<PlayerAttacks>();

        //InItLockAction();

        //�׽�Ʈ�� true
        lockAction[Paction.AirAttack] = true;
        lockAction[Paction.ChargeShot] = true;
        lockAction[Paction.Dash] = true;
        lockAction[Paction.DoubleJump] = true;
        lockAction[Paction.MeleeAttack] = true;
        lockAction[Paction.RangeAttack] = true;
        lockAction[Paction.JumpAttack] = true;
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
        lockAction.Add(Paction.DoubleJump,false);
        lockAction.Add(Paction.MeleeAttack,false);
        lockAction.Add(Paction.RangeAttack,false);
        lockAction.Add(Paction.JumpAttack, false);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        // �Է� ���͸� ������Ʈ
        float inputX = context.ReadValue<Vector2>().x;

        if (Keyboard.current.aKey.isPressed == true && Keyboard.current.dKey.isPressed == true)
            return; // ����Ű ���� �Է� �� ���� ���� ���� �켱 �̵�

        _inputVector = new Vector2(inputX, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_playerMovement.HasJumped()) 
            {
                _playerMovement.Jump();
            }
            else if (_playerMovement.HasJumped() && lockAction[Paction.DoubleJump]) 
            {
                _playerMovement.DoubleJump();
            }
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && lockAction[Paction.Dash]) // ��� ��ư�� ������ ����
        {
            _playerMovement.Dash();
        }
    }


    public void OnAttack(InputAction.CallbackContext context) // ���� ����
    {
        if (context.started && !_isWeaponChange && lockAction[Paction.ChargeShot])
        {
            _playerAttacks.StartCharging();
        }

        if (context.performed)
        {
            if (!_isWeaponChange && lockAction[Paction.RangeAttack])
            {
                _playerAttacks.Fire();
            }
            else if (_isWeaponChange && lockAction[Paction.MeleeAttack])
            {
                _playerAttacks.Attack();
            }
            else if (_isWeaponChange && !_playerMovement.IsGround() && lockAction[Paction.JumpAttack])
            {
                _playerAttacks.JumpAttack();
            }
        }

        else if (context.canceled && !_isWeaponChange && lockAction[Paction.ChargeShot])
        {
            _playerAttacks.ReleaseCharge(); 
        }
    }

    public void OnSwap(InputAction.CallbackContext context) // ��,���Ÿ� ���� ����
    {
        if (context.performed)
        {
            _isWeaponChange = !_isWeaponChange;
            Debug.Log(_isWeaponChange ? "�ٰŸ� ����" : "���Ÿ� ����");
        }
    }

    public void OnDeflect(InputAction.CallbackContext context) // �ݻ�
    {

    }


    public void OnInteraction(InputAction.CallbackContext context)
    {

    }

    public void OnInventory(InputAction.CallbackContext context)
    {

    }

    public void OnDown(InputAction.CallbackContext context) // �Ʒ� ����
    {
        bool isPressing = context.ReadValue<float>() > 0;
        _playerMovement.SetIsPressingDown(isPressing);
    }

    public void UnLockAction(Paction unLockAction)
    {
        lockAction[unLockAction] = true;
       // if (unLockAction == Paction.DoubleJump)
       //{
       //     _playerMovement.SetDoubleJumpEnabled(lockAction[unLockAction]);
       // }
    }
}
