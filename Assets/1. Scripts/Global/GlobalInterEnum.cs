using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������̽��� �տ� I�� ���� able�� ���̴°� �Ϲ����Դϴ�.
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
    DoubleJump,
    Dash,
    RangeAttack,
    ChargeShot,
    JumpAttack,
    Deflect,
    ComboAttack
}
public enum Monsters
{
    Bear,Frog,Rat,MushRoom,IceGolem,Eagle,Dragon
}