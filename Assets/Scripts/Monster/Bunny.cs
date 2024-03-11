using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            rb.velocity = new Vector2(Move, rb.velocity.y); //몬스터 기본 움직임

            if (!Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask) &&  //벽체크 0번 플랫폼이없고
                 Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask) /*&&  //1번이 플랫폼이면 몬스터 점프
                !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask)*/)  //플랫폼과 너무 가까우면 올라가기 힘들기 때문에 넣음
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if (Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask))
            {
                MonsterFlip();
            }
            else if (!Physics2D.OverlapCircle(wallCheck[2].position, 0.01f, layerMask) &&
                     !Physics2D.OverlapCircle(wallCheck[3].position, 0.01f, layerMask) &&
                     !Physics2D.OverlapCircle(wallCheck[4].position, 0.01f, layerMask))
            {
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
