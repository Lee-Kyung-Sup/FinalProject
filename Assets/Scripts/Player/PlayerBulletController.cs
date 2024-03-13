using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] private GameObject RangeHitEffect; // 히트 효과 프리팹

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyTime", 2.0f);
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("벽에 충돌!");
            Instantiate(RangeHitEffect, transform.position, Quaternion.identity); // 히트 효과 생성
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("몬스터에게 충돌!");
            Instantiate(RangeHitEffect, transform.position, Quaternion.identity); // 히트 효과 생성
            // collision.SendMessage("Demaged", 10); // Demaged 함수 호출, 원거리 공격력(10, 임시)만큼 피해  TODO
            Destroy(gameObject);
        }
    }
}
