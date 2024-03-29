using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttackHandler : MonoBehaviour
{
    [Header("Range Attack Parameters")]
    [SerializeField] private GameObject RangeHitEffect; // ��Ʈ ȿ�� ������
    [SerializeField] private bool isChargeShot = false; // ������ �������� true

    PlayerStatus playerStatus;
    private List<Coroutine> hitCoroutines = new List<Coroutine>();

    void Start()
    {
        Invoke("DestroyTime", 7.0f);
        playerStatus = GetComponent<PlayerStatus>();
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & ((1 << 8) | (1 << 9))) != 0) // 8: �׶��� , 9: �÷���
        {
            if (!isChargeShot) // �Ϲ� ���� ��쿡�� ���̳� �÷����� �ε����� �� �����
            {
                Instantiate(RangeHitEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        else if (((1 << collision.gameObject.layer) & (1 << 7)) != 0) // 7: ����
        {
            if (isChargeShot) // ���� ��
            {
                Coroutine hitCoroutine = StartCoroutine(MultiHit(collision));
                hitCoroutines.Add(hitCoroutine);
            }
            else // �Ϲ� ��
            {
                HitEffectAndDamage(collision);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator MultiHit(Collider2D collision)
    {
        while (collision != null && gameObject != null)
        {
            HitEffectAndDamage(collision);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & (1 << 7)) != 0) // 7: ����
        {
            StopAllCoroutines();
            hitCoroutines.Clear();
        }
    }

    private void HitEffectAndDamage(Collider2D collision)
    {
        Vector3 bulletColliderCenter = GetComponent<Collider2D>().bounds.center;
        Vector3 monsterColliderCenter = collision.bounds.center;

        Vector3 directionToMonster = (monsterColliderCenter - bulletColliderCenter).normalized;

        Vector3 hitEffectPosition = monsterColliderCenter - directionToMonster * 0.2f;

        Instantiate(RangeHitEffect, hitEffectPosition, Quaternion.identity);

        // collision.SendMessage("Damaged", 1);
    }
}
