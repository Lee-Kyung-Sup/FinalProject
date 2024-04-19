using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    private PlayerMovement _playerMovement;
    private PlayerAttacks _playerAttacks;

    private Vector2 _inputVector; 
    //private bool _isWeaponChange = true; // true �ٰŸ�, false ���Ÿ� ����

    Dictionary<Paction, bool> lockAction = new Dictionary<Paction, bool>();
    public Dictionary<Paction, bool> LockAction
    {
        get { return lockAction; }
    }

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAttacks = GetComponent<PlayerAttacks>();

        //InItLockAction();

        //�׽�Ʈ��
        TestingOnActions();
    }

    void FixedUpdate()
    {
        _playerMovement.Move(_inputVector.x);
    }

    void InItLockAction()
    {
        lockAction.Add(Paction.ChargeShot,false);
        lockAction.Add(Paction.Dash,false);
        lockAction.Add(Paction.DoubleJump,false);
        lockAction.Add(Paction.RangeAttack,false);
        lockAction.Add(Paction.JumpAttack, false);
        lockAction.Add(Paction.Deflect, false);
        lockAction.Add(Paction.ComboAttack, false);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        //AudioManager.Instance.PlaySFX("Step"); // �����Ҹ� JHP

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
                //AudioManager.Instance.PlaySFX("Jump"); // �����Ҹ� JHP
            }
            else if (_playerMovement.HasJumped() && lockAction[Paction.DoubleJump]) 
            {
                _playerMovement.DoubleJump();
                //AudioManager.Instance.PlaySFX("Jump"); // �����Ҹ� JHP
            }
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && lockAction[Paction.Dash]) // ��� ��ư�� ������ ����
        {
            _playerMovement.Dash();
            //AudioManager.Instance.PlaySFX("Dash"); // ��üҸ� JHP
        }
    }

    public void OnAttack(InputAction.CallbackContext context) // ���� ����
    {
        if (context.performed)
        {
                _playerAttacks.MeleeAttack();
                //AudioManager.Instance.PlaySFX("Attack"); // �������ݼҸ� JHP

            if (!_playerMovement.IsGround() && lockAction[Paction.JumpAttack])
            {
                _playerAttacks.JumpAttack();
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started && lockAction[Paction.RangeAttack])
        {
            _playerAttacks.StartCharging();
        }

        if (context.performed && lockAction[Paction.RangeAttack])
        {
            _playerAttacks.Fire();
        }

        else if (context.canceled && lockAction[Paction.ChargeShot])
        {
            _playerAttacks.ReleaseCharge();
        }
    }


    public void OnDeflect(InputAction.CallbackContext context) // �ݻ�
    {
        if(context.performed && lockAction[Paction.Deflect])
        {
            _playerAttacks.Deflect();
        }
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
    }

    void TestingOnActions()
    {
        lockAction[Paction.ChargeShot] = true;
        lockAction[Paction.Dash] = true;
        lockAction[Paction.DoubleJump] = true;
        lockAction[Paction.RangeAttack] = true;
        lockAction[Paction.JumpAttack] = true;
        lockAction[Paction.Deflect] = true;
        lockAction[Paction.ComboAttack] = true;
    }


    //public void OnSwap(InputAction.CallbackContext context) // ��,���Ÿ� ���� ����
    //{
    //    if (context.performed)
    //    {
    //        
    //        _isWeaponChange = !_isWeaponChange;
    //        Debug.Log(_isWeaponChange ? "�ٰŸ� ����" : "���Ÿ� ����");
    //    }
    //}
}
