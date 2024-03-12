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
        while(true)
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
        while(runTime >= 0f)
        {
            runTime -= Time.deltaTime;
            if (!isHit)
            {
                rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);


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
                     Physics2D.OverlapCircle(wallCheck[1].position, 0.35f, layerMask[0])))
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
        if(currentState != State.Attack &&
           currentState != State.Jump)
        {
            if(!IsPlayerDir())
            {
                MonsterFlip();
            }
        }
    } 

    IEnumerator Attack()
    {
        yield return null;
        if(!isHit && isGround)
        {
            capsuleCollider.offset = capsuleColliderJumpOffset;
            canAtk = false;
            rb.velocity = new Vector2(-transform.localScale.x * 10f, jumpPower/ 1.25f);
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
        if(!isHit && isGround && !IsPlayingAnim("Run"))
        {
            capsuleCollider.offset = capsuleColliderOffset;
            MyAnimSetTrigger("Idle");
        }
    }
}

