using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeflectBullet : MonoBehaviour
{
    public AttackTypes attackType;
    public int damage;

    public void Initialize(AttackTypes attackType, int damage)
    {
        this.attackType = attackType;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & (1 << 7)) != 0) // 7. 몬스터 레이어 
        {
            GameObject effect = ObjectManager.Instance.MakeObj("PlayerRangeHit");

            if(effect != null )
            {
                effect.transform.position = GetHitEffectPosition(collision);
                effect.transform.rotation = Quaternion.identity;
                effect.SetActive(true);
            }

            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
        gameObject.SetActive(false);
    }

    private Vector3 GetHitEffectPosition(Collider2D collision) // 히트 이펙트 효과
    {
        Vector3 bulletColliderCenter = GetComponent<Collider2D>().bounds.center;
        Vector3 collisionColliderCenter = collision.bounds.center;
        Vector3 directionToCollision = (collisionColliderCenter - bulletColliderCenter).normalized;
        return collisionColliderCenter - directionToCollision * 0.2f; // 효과 위치를 약간 조정
    }
}
