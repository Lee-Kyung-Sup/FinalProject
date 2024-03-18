using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : Monster
{
    public enum State
    {
        Idle,
        Run,
        Attack,
        Jump
    };

    public State currentState = State.Idle;

    public Transform[] wallCheck;
    WaitForSeconds Delay500 = new WaitForSeconds(0.5f);

    Vector2 capsuleColliderOffset;
    Vector2 capsuleColliderJumpOffset;

    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 3f;
        jumpPower = 10f;
        currentHp = 6;
        atkCoolTime = 3f;
        atkCoolTimeCalc = atkCoolTime;

        capsuleColliderOffset = capsuleCollider.offset;
        capsuleColliderJumpOffset = new Vector2(capsuleColliderOffset.x, 1f);

        StartCoroutine(FSM());
    }

    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }
    }

    IEnumerator Idle()
    {
        capsuleCollider.offset = capsuleColliderOffset;
        yield return Delay500;
        currentState = State.Run;
    }

    IEnumerator Run()
    {
        yield return null;
        float runTime = Random.Range(2f, 4f);
        while (runTime >= 0f)
        {
            runTime -= Time.deltaTime;
            if (!isHit)
            {
                rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);


                if ((!Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) &&  //벽체크 0번 플랫폼이없고
                      Physics2D.OverlapCircle(wallCheck[1].position, 0.1f, layerMask)))  /*&&  //1번이 플랫폼이면 몬스터 점프
                       !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask)*/  //플랫폼과 너무 가까우면 올라가기 힘들기 때문에 넣음
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


                Vector2 monsterFrontBelowPosition = (Vector2)transform.localPosition + new Vector2(-transform.localScale.x, -1f);

                Vector2 origin = monsterFrontBelowPosition;

                Vector2 direction = Vector2.down;

                float distance = 3f;

                // Raycast 시각적으로 표시
                Debug.DrawRay(origin, direction * distance, Color.red);

                // Raycast를 사용하여 조건 확인
                if (CheckIfNoWall(origin, direction, distance, layerMask))
                {
                    Debug.Log("t2");

                    MonsterFlip();
                }

                if (canAtk && IsPlayerDir() && isGround)
                {
                    if (Vector2.Distance(transform.position, GameManager.instance.GetPlayerPosition()) < 5f)
                    {
                        currentState = State.Attack;
                        break;
                    }
                }
            }
            yield return null;
        }
        if (currentState != State.Attack &&
           currentState != State.Jump)
        {
            if (!IsPlayerDir())
            {
                MonsterFlip();
            }
        }
    }

    IEnumerator Attack()
    {
        yield return null;
        if (!isHit && isGround)
        {
            capsuleCollider.offset = capsuleColliderJumpOffset;
            canAtk = false;
            rb.velocity = new Vector2(-transform.localScale.x * 14f, jumpPower / 1.25f);
            MyAnimSetTrigger("Attack");

            yield return Delay500;
            currentState = State.Idle;
        }
        else
        {
            currentState = State.Run;
        }

    }
    IEnumerator Jump()
    {
        yield return null;
        capsuleCollider.offset = capsuleColliderJumpOffset;

        rb.velocity = new Vector2(-transform.localScale.x * 6f, jumpPower);
        MyAnimSetTrigger("Attack");

        yield return Delay500;
        currentState = State.Idle;
    }

    void Update()
    {
        GroundCheck();
        if (!isHit && isGround && !IsPlayingAnim("Run"))
        {
            capsuleCollider.offset = capsuleColliderOffset;
            MyAnimSetTrigger("Idle");
        }
    }
}

