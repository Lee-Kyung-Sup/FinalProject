using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackHandler : MonoBehaviour
{
    [Header("Melee Attack Parameters")]
    [SerializeField] private GameObject meleeHitEffect; // ��Ʈ ȿ�� ������
    [SerializeField] private float hitInterval = 0.25f;

    public PlayerAttacks playerAttacks;
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

            // �� �߽� ���� ������ ���� ���� ���
            Vector3 directionToMonster = (monsterColliderCenter - playerColliderCenter).normalized;

            Vector3 hitEffectPosition = monsterColliderCenter - directionToMonster * hitInterval;

            Instantiate(meleeHitEffect, hitEffectPosition, Quaternion.identity);

            AttackTypes attackType = playerAttacks.currentAttackType;
            int damage = playerStatus.attackPower[attackType];
            collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);

            Debug.Log($"{collision.gameObject.name}���� {attackType} ���� {damage}�� ������");
        }
    }
}

