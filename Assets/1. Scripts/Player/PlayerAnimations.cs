using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerAnimations : MonoBehaviour
{
    protected Animator[] animator;

    protected virtual void Awake()
    {
        animator = GetComponentsInChildren<Animator>();
    }

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsFalling = Animator.StringToHash("IsFalling");
    private static readonly int IsCharging = Animator.StringToHash("IsCharging");

    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Dash = Animator.StringToHash("Dash");

    private static readonly int Fire = Animator.StringToHash("Fire");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int JumpAtk = Animator.StringToHash("JumpAtk");
    private static readonly int JumpAtkEffect = Animator.StringToHash("JumpAtkEffect");
    private static readonly int Deflect = Animator.StringToHash("Deflect");

    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Invincible = Animator.StringToHash("Invincible");
    private static readonly int Die = Animator.StringToHash("Die");

    private static readonly int ShotEffect = Animator.StringToHash("ShotEffect");


    public void Moving(bool isMoving)
    {
        animator[0].SetBool(IsMoving, isMoving);
    }

    public void Falling(bool isFalling)
    {
        animator[0].SetBool(IsFalling, isFalling);
    }

    public void Charging(bool isCharging)
    {
        animator[2].SetBool(IsCharging, isCharging);
    }
    public void Deflection()
    {
        animator[0].SetTrigger(Deflect);
        animator[4].SetTrigger(Deflect);
    }

    public void Jumping()
    {
        animator[0].SetTrigger(Jump);
    }

    public void Dashing()
    {
        animator[0].SetTrigger(Dash);
    }

    public void Attacking()
    {
        animator[0].SetTrigger(Attack);
    }

    public void JumpAttacking()
    {
        animator[0].SetTrigger(JumpAtk);
    }
    public void JumpAttackEffect()
    {
        animator[1].SetTrigger(JumpAtkEffect);
    }

    public void Fired()
    {
        animator[0].SetTrigger(Fire);
    }

    public void FireEffect()
    {
        animator[3].SetTrigger(ShotEffect);
    }

    public void GetHit()
    {
        animator[0].SetTrigger(Hit);
    }
    public void InvincibleEffect() // ¹«Àû ÀÌÆåÆ® 
    {
        animator[0].SetTrigger(Invincible);
    }

    public void Dead()
    {
        animator[0].SetTrigger(Die);
    }




}
