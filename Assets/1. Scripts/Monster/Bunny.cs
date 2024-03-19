using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
public class Bunny : Monster
{
    public Transform[] wallCheck;
    public float move;

    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 2f;
        jumpPower = 15f;
    }

    void Update()
    {
        if (!isHit && monsterIdle)
        {
            move = -transform.localScale.x * moveSpeed;
            rb.velocity = new Vector2(move, rb.velocity.y); //���� �⺻ ������


            if ((!Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) &&  //��üũ 0�� �÷����̾���
                  Physics2D.OverlapCircle(wallCheck[1].position, 0.1f, layerMask))  /*&&  //1���� �÷����̸� ���� ����
                       !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask)*/)  //�÷����� �ʹ� ������ �ö󰡱� ����� ������ ����
            {
                
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                              
                Debug.Log(rb.velocity);
            }
            if ((Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) &&
                 Physics2D.OverlapCircle(wallCheck[1].position, 0.1f, layerMask))
            )
            {
                Debug.Log("t1");
                MonsterFlip();
            }

            
            Vector2 monsterFrontBelowPosition = (Vector2)transform.localPosition + new Vector2(-transform.localScale.x * 0.2f, 0);

            Vector2 origin = monsterFrontBelowPosition;

            Vector2 direction = Vector2.down;

            float distance = 3f;

            // Raycast �ð������� ǥ��
            Debug.DrawRay(origin, direction * distance, Color.red);

            // Raycast�� ����Ͽ� ���� Ȯ��
            if (CheckIfNoWall(origin, direction, distance, layerMask))
            {
                Debug.Log("t2");
               
                MonsterFlip();
            }


        }
    }
  

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.transform.tag == ("PlayerHitBox"))
        {
            MonsterFlip();
        }
    }
}
