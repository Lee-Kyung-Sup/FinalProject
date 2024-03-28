using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyAnimationTriggers : MonoBehaviour
{
    EnemyIceGolem enemy => GetComponentInParent<EnemyIceGolem>();
    void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    void AttackTrigger()
    {
        Collider2D col = Physics2D.OverlapCircle(enemy.attackCheck.position, enemy.attackCheckRadius);
        if (col.TryGetComponent<IDamageable>(out IDamageable a))
        {
            a.TakeDamage(1);
        }
    }
}