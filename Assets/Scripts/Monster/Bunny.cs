using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bunny : Monster
{
    public Transform[] wallCheck;
    public float Move;

    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 2f;
        jumpPower = 15f;
    }

    void Update()
    {
        if (!isHit)
        {
            Move = -transform.localScale.x * moveSpeed;
            rb.velocity = new Vector2(Move, rb.velocity.y); //���� �⺻ ������


            if (!Physics2D.OverlapCircle(wallCheck[0].position, 0.35f, layerMask[1]) &&  //��üũ 0�� �÷����̾���
                 Physics2D.OverlapCircle(wallCheck[1].position, 0.35f, layerMask[0]) /*&&  //1���� �÷����̸� ���� ����
                    !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask)*/)  //�÷����� �ʹ� ������ �ö󰡱� ����� ������ ����
            {

                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                Debug.Log(rb.velocity);
            }
            if ((Physics2D.OverlapCircle(wallCheck[0].position, 0.35f, layerMask[1]) &&
                 Physics2D.OverlapCircle(wallCheck[1].position, 0.35f, layerMask[1])) &&
                (Physics2D.OverlapCircle(wallCheck[0].position, 0.35f, layerMask[0]) &&
                 Physics2D.OverlapCircle(wallCheck[1].position, 0.35f, layerMask[0]))
            )
            {
                Debug.Log("t1");
                MonsterFlip();
            }
            if ((!Physics2D.OverlapCircle(wallCheck[2].position, 0.35f, layerMask[1]) &&
                !Physics2D.OverlapCircle(wallCheck[3].position, 0.35f, layerMask[1]) &&
                !Physics2D.OverlapCircle(wallCheck[4].position, 0.35f, layerMask[1])) &&
                (!Physics2D.OverlapCircle(wallCheck[2].position, 0.35f, layerMask[0]) &&
                !Physics2D.OverlapCircle(wallCheck[3].position, 0.35f, layerMask[0]) &&
                !Physics2D.OverlapCircle(wallCheck[4].position, 0.35f, layerMask[0])))
                
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
