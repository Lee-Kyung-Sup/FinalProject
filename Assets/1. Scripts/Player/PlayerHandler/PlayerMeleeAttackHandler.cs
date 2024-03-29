using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackHandler : MonoBehaviour
{
    [Header("Melee Attack Parameters")]
    [SerializeField] private GameObject meleeHitEffect; // 히트 효과 프리팹
    [SerializeField] private float hitInterval = 0.25f;

    PlayerStatus playerStatus;

    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & (1 << 7)) != 0)
        {
            Vector3 playerColliderCenter = this.GetComponent<Collider2D>().bounds.center;
            Vector3 monsterColliderCenter = collision.bounds.center;

            // 두 중심 지점 사이의 방향 벡터 계산
            Vector3 directionToMonster = (monsterColliderCenter - playerColliderCenter).normalized;

            Vector3 hitEffectPosition = monsterColliderCenter - directionToMonster * hitInterval;

            Instantiate(meleeHitEffect, hitEffectPosition, Quaternion.identity);

            // 몬스터에게 데미지를 주는 로직 (추가적인 구현 필요)
            //collision.SendMessage("TakeDamage", playerStatus.Atk);
        }
    }
}

