using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 3; // 캐릭터 체력
    [SerializeField] private PlayerUI healthUI;

    [SerializeField] private int stamina = 100; // 캐릭터 스태미너
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 20f;
    [SerializeField] private int maxJumpCount = 2; // 최대 점프 가능 횟수

    public float Speed => speed;
    public float JumpPower => jumpPower;
    public int MaxJumpCount => maxJumpCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        healthUI.UpdateHeartUI(health);

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        
        Debug.Log("플레이어 죽음");
        // 플레이어 사망 후 로직 TODO - 입력 불가, DIE 애니메이션 재생, Game Over Scene으로.
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Monster"))
        {
            TakeDamage(1);
        }
    }


}
