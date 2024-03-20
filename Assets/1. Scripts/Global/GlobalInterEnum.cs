using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//인터페이스는 앞에 I나 끝에 able을 붙이는게 일반적입니다.
public interface IsGroundable
{
    public bool IsGround();
}
public interface IDamageable
{
    void TakeDamage(int damage);
}
public enum Paction
{
    MeleeAttack,
    AirAttack,
    DoubleJump,
    Dash,
    RangeAttack,
    ChargeShot
}