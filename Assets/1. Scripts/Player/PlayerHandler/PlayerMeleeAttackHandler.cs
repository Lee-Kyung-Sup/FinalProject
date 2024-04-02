using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackHandler : MonoBehaviour
{
    [Header("Melee Attack Parameters")]
    [SerializeField] private GameObject meleeHitEffect; 
    [SerializeField] private GameObject JumpHitEffect;
    [SerializeField] private float hitInterval = 0.25f;

    PlayerAttacks playerAttacks;
    PlayerStatus playerStatus;

    void Start()
    {
        playerAttacks = GetComponentInParent<PlayerAttacks>();
        playerStatus = GetComponentInParent<PlayerStatus>();
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


            string effectType = "";
            switch (playerAttacks.currentAttackType)
            {
                case AttackTypes.MeleeAttack:
                    effectType = "PlayerMeleeHit";
                    break;
                case AttackTypes.JumpAttack:
                    effectType = "PlayerJumpHit";
                    break;
            }

            GameObject effect = ObjectManager.Instance.MakeObj(effectType);
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
    }
}

