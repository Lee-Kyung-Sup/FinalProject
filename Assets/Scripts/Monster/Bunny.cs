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
        if (!isHit)
        {
            move = -transform.localScale.x * moveSpeed;
            rb.velocity = new Vector2(move, rb.velocity.y); //몬스터 기본 움직임


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

            // 몬스터의 앞 아래 위치 계산
            Vector2 monsterFrontBelowPosition = (Vector2)transform.position + new Vector2(-1f, -1f);
            // Raycast 시작점
            Vector2 origin = monsterFrontBelowPosition;

            // Raycast 방향 (아래 방향)
            Vector2 direction = monsterDirRight ? Vector2.down : Vector2.down;

            // Raycast 길이
            float distance = 3f;

            // Raycast 시각적으로 표시
            Debug.DrawRay(origin, direction * distance, Color.red);

            // Raycast를 사용하여 조건 확인
            if (CheckIfNoWall(origin, direction, distance, layerMask[1]) && CheckIfNoWall(origin, direction, distance, layerMask[0]))
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
