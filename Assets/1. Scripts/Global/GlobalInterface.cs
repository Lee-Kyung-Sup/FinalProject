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

