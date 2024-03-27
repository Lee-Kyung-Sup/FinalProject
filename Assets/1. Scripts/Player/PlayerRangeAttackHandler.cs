using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttackHandler : MonoBehaviour
{
    [SerializeField] private GameObject RangeHitEffect; // 히트 효과 프리팹

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyTime", 7.0f);
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            Instantiate(RangeHitEffect, transform.position, Quaternion.identity); // 히트 효과 생성
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Monster"))
        {
            Instantiate(RangeHitEffect, transform.position, Quaternion.identity); // 히트 효과 생성
            // collision.SendMessage("Demaged", 1); // Demaged 함수 호출, 원거리 공격력(1, 임시)만큼 피해  TODO
            Destroy(gameObject);
        }
    }
}
