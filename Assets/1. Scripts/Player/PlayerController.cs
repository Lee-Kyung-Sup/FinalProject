using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    private PlayerMovement _playerMovement;
    private PlayerAttacks _playerAttacks;

    private Vector2 _inputVector; 
    //private bool _isWeaponChange = true; // true 근거리, false 원거리 공격

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

        //테스트용
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
        //AudioManager.Instance.PlaySFX("Step"); // 걸음소리 JHP

        // 입력 벡터를 업데이트
        float inputX = context.ReadValue<Vector2>().x;

        if (Keyboard.current.aKey.isPressed == true && Keyboard.current.dKey.isPressed == true)
            return; // 방향키 동시 입력 시 먼저 누른 방향 우선 이동
 
        _inputVector = new Vector2(inputX, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_playerMovement.HasJumped()) 
            {
                _playerMovement.Jump();
                //AudioManager.Instance.PlaySFX("Jump"); // 점프소리 JHP
            }
            else if (_playerMovement.HasJumped() && lockAction[Paction.DoubleJump]) 
            {
                _playerMovement.DoubleJump();
                //AudioManager.Instance.PlaySFX("Jump"); // 점프소리 JHP
            }
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && lockAction[Paction.Dash]) // 대시 버튼이 눌렸을 때만
        {
            _playerMovement.Dash();
            //AudioManager.Instance.PlaySFX("Dash"); // 대시소리 JHP
        }
    }

    public void OnAttack(InputAction.CallbackContext context) // 근접 공격
    {
        if (context.performed)
        {
                _playerAttacks.MeleeAttack();
                //AudioManager.Instance.PlaySFX("Attack"); // 근접공격소리 JHP

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


    public void OnDeflect(InputAction.CallbackContext context) // 반사
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

    public void OnDown(InputAction.CallbackContext context) // 아래 점프
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


    //public void OnSwap(InputAction.CallbackContext context) // 근,원거리 공격 스왑
    //{
    //    if (context.performed)
    //    {
    //        
    //        _isWeaponChange = !_isWeaponChange;
    //        Debug.Log(_isWeaponChange ? "근거리 무기" : "원거리 무기");
    //    }
    //}
}
