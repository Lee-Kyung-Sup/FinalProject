using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerAnimations : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");

    private static readonly int Dash = Animator.StringToHash("Dash");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Fire = Animator.StringToHash("Fire");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Invincible = Animator.StringToHash("Invincible");
    private static readonly int Die = Animator.StringToHash("Die");


    public void Moving(bool isMoving)
    {
        animator.SetBool(IsMoving, isMoving);
    }

    public void Jumping(bool isJumping)
    {
        animator.SetBool(IsJumping, isJumping);
    }


    public void Dashing()
    {
        animator.SetTrigger(Dash);
    }

    public void Attacking()
    {
        animator.SetTrigger(Attack);
    }

    public void Fired()
    {
        animator.SetTrigger(Fire);
    }

    public void GetHit()
    {
        animator.SetTrigger(Hit);
    }
    public void InvincibleEffect() // 무적 이펙트 애니메이션 (피격 시)
    {
        animator.SetTrigger(Invincible);
    }

    public void Dead()
    {
        animator.SetTrigger(Die);
    }
}
