using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackHandler : MonoBehaviour
{
    [SerializeField] private GameObject meleeHitEffect; // 히트 효과 프리팹
    PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable a))
        {
            a.TakeDamage((int)playerStatus.Atk);
        }


            if (collision.CompareTag("Monster"))
            {
                Instantiate(meleeHitEffect, collision.transform.position, Quaternion.identity);

            //collision.SendMessage("TakeDamage", playerStatus.Atk);  // 몬스터에게 데미지
        }

    }
}
