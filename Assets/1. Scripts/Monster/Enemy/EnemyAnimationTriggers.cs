using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyAnimationTriggers : MonoBehaviour
{
    EnemyMelee enemy => GetComponentInParent<EnemyMelee>();
    LayerMask pLayer;
    private void Start()
    {
        pLayer = LayerMask.GetMask("Player");
    }
    void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    void AttackTrigger()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        foreach (var item in col)
        {
            if (pLayer.value == (pLayer.value | (1 << item.gameObject.layer)))
            {
                if (item.TryGetComponent<IDamageable>(out IDamageable a))
                {
                    a.TakeDamage(enemy.dmg);
                }
            }
        }
    }
}