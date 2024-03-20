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
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    private static readonly int Dash = Animator.StringToHash("Dash");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Fire = Animator.StringToHash("Fire");
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

    public void Hit()
    {
        animator.SetBool(IsHit, true);
    }
    public void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}
