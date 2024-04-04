using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttackHandler : MonoBehaviour
{
    [Header("Range Attack Parameters")]
    [SerializeField] private GameObject RangeHitEffect; // 히트 효과 프리팹
    [SerializeField] private bool isChargeShot = false; // 차지샷 프리팹은 true

    public PlayerStatus playerStatus;
    public PlayerAttacks playerAttacks;
    private List<Coroutine> hitCoroutines = new List<Coroutine>();

    private void OnEnable()
    {
        StartCoroutine(DisableBulletPrefabs(3.0f));
    }

    IEnumerator DisableBulletPrefabs(float time)
    {
        yield return new WaitForSeconds(time);
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & ((1 << 8) | (1 << 9))) != 0) // 8: 그라운드 , 9: 플랫폼
        {
            if (!isChargeShot) // 일반 샷인 경우에만 벽이나 플랫폼에 부딪혔을 때 사라짐
            {
                GameObject effect = ObjectManager.Instance.MakeObj("PlayerRangeHit");
                if (effect != null)
                {
                    effect.transform.position = transform.position;
                    effect.transform.rotation = Quaternion.identity;
                    effect.SetActive(true);
                }
                gameObject.SetActive(false);
            }
        }

        else if (((1 << collision.gameObject.layer) & (1 << 7)) != 0) // 7: 몬스터
        {
            if (isChargeShot) // 차지 샷
            {
                Coroutine hitCoroutine = StartCoroutine(MultiHit(collision));
                hitCoroutines.Add(hitCoroutine);
            }
            else // 일반 샷
            {
                HitEffectAndDamage(collision);
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator MultiHit(Collider2D collision)
    {
        while (collision != null && gameObject.activeSelf)
        {
            HitEffectAndDamage(collision);
            yield return new WaitForSeconds(0.05f);
        }
    }


    private void HitEffectAndDamage(Collider2D collision) // 히트효과 & 데미지 적용
    {
        Vector3 hitEffectPosition = GetHitEffectPosition(collision);

        GameObject effect = ObjectManager.Instance.MakeObj("PlayerRangeHit"); 
        if (effect != null)
        {
            effect.transform.position = hitEffectPosition;
            effect.transform.rotation = Quaternion.identity;
            effect.SetActive(true);
        }

        AttackTypes attackType = playerAttacks.currentAttackType;
        int damage = playerStatus.attackPower[attackType];
        collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);

        Debug.Log($"{collision.gameObject.name}에게 {attackType} 공격 {damage}의 데미지");
    }

    private Vector3 GetHitEffectPosition(Collider2D collision) // 히트 효과 위치 조정
    {
        Vector3 bulletColliderCenter = GetComponent<Collider2D>().bounds.center;
        Vector3 monsterColliderCenter = collision.bounds.center;
        Vector3 directionToMonster = (monsterColliderCenter - bulletColliderCenter).normalized;
        return monsterColliderCenter - directionToMonster * 0.2f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & (1 << 7)) != 0) // 7: 몬스터
        {
            foreach (var hitCoroutine in hitCoroutines)
            {
                StopCoroutine(hitCoroutine);
            }
            hitCoroutines.Clear();
        }
    }
}
