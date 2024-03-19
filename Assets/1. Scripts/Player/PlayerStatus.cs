using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 3; // ĳ���� ü��
    [SerializeField] private PlayerUI healthUI;

    [SerializeField] private int stamina = 100; // ĳ���� ���¹̳�
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 20f;
    [SerializeField] private int maxJumpCount = 2; // �ִ� ���� ���� Ƚ��

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
        
        Debug.Log("�÷��̾� ����");
        // �÷��̾� ��� �� ���� TODO - �Է� �Ұ�, DIE �ִϸ��̼� ���, Game Over Scene����.
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Monster"))
        {
            TakeDamage(1);
        }
    }


}
