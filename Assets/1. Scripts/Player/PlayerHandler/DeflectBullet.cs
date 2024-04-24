using System.Collections;
using UnityEngine;

public class DeflectBullet : MonoBehaviour
{
    public AttackTypes attackType;
    public int damage;
    private Coroutine timeoutCoroutine;

    public void Initialize(AttackTypes attackType, int damage)
    {
        this.attackType = attackType;
        this.damage = damage;

        if (timeoutCoroutine != null)
        {
            StopCoroutine(timeoutCoroutine);
        }

        timeoutCoroutine = StartCoroutine(ResetAfterDelay(5f));
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetToEnemyBullet();
        Debug.Log("적 투사체 자동 리턴");
    }

    public void ResetToEnemyBullet() // 적 투사체 복구 메서드
    {
        gameObject.layer = 12; // EnemyBullet 레이어로 복구
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & (1 << 7)) != 0) // 7: 몬스터
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                AudioManager.Instance.PlaySFX("RangeHit");
            }
            ResetToEnemyBullet();
        }
    }
}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (((1 << collision.gameObject.layer) & (1 << 20)) != 0) // 20. 플레이어 투사체 레이어
    //    {
    //        GameObject effect = ObjectManager.Instance.MakeObj("PlayerRangeHit");

    //        if(effect != null )
    //        {
    //            Debug.Log("이펙트 생성 성공");
    //            effect.transform.position = GetHitEffectPosition(collision);
    //            effect.transform.rotation = Quaternion.identity;
    //            effect.SetActive(true);
    //        }

    //        IDamageable damageable = collision.GetComponent<IDamageable>();
    //        if (damageable != null)
    //        {
    //            damageable.TakeDamage(damage);
    //        }

    //        gameObject.SetActive(false);
    //    }

    //}

    //private Vector3 GetHitEffectPosition(Collider2D collision) // 히트 이펙트 효과
    //{
    //    Vector3 bulletColliderCenter = GetComponent<Collider2D>().bounds.center;
    //    Vector3 collisionColliderCenter = collision.bounds.center;
    //    Vector3 directionToCollision = (collisionColliderCenter - bulletColliderCenter).normalized;
    //    return collisionColliderCenter - directionToCollision * 0.2f; // 효과 위치를 약간 조정
    //}

