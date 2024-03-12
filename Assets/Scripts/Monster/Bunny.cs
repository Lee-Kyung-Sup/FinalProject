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
            rb.velocity = new Vector2(Move, rb.velocity.y); //몬스터 기본 움직임


            if (!Physics2D.OverlapCircle(wallCheck[0].position, 0.35f, layerMask[1]) &&  //벽체크 0번 플랫폼이없고
                 Physics2D.OverlapCircle(wallCheck[1].position, 0.35f, layerMask[0]) /*&&  //1번이 플랫폼이면 몬스터 점프
                    !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask)*/)  //플랫폼과 너무 가까우면 올라가기 힘들기 때문에 넣음
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
