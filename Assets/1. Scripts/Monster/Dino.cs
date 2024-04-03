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
        //Jump
    };

    public State currentState = State.Idle;

    public Transform[] wallCheck;
    WaitForSeconds Delay500 = new WaitForSeconds(0.55f);

    //Vector2 capsuleColliderOffset;
    //Vector2 capsuleColliderJumpOffset;
    
    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 3f;
        jumpPower = 10f;
        currentHp = 6;
        atkCoolTime = 3f;
        atkCoolTimeCalc = atkCoolTime;

        //capsuleColliderOffset = capsuleCollider.offset;
        //capsuleColliderJumpOffset = new Vector2(capsuleColliderOffset.x, 1f);

        StartCoroutine(FSM());
    }
    protected void GroundCheck()
    {
        Debug.DrawRay(transform.localPosition, Vector2.down * 3f, Color.red);
        if (Physics2D.Raycast(transform.localPosition, Vector2.down, 3f, layerMask))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

    }
    protected void Update()
    {
        GroundCheck();
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
        //yield return null;

        //capsuleCollider.offset = capsuleColliderOffset;
        MyAnimSetTrigger(currentState.ToString());
        yield return Delay500;
        currentState = State.Run;
    }

    IEnumerator Run()
    {
        yield return null;
        float runTime = Random.Range(2f, 4f);
        while (runTime >= 0f)
        {
            MyAnimSetTrigger(currentState.ToString());
            runTime -= Time.deltaTime;
            if (!base.Hit)
            {
                rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);


                if ((!Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) &&  //��üũ 0�� �÷����̾���
                      Physics2D.OverlapCircle(wallCheck[1].position, 0.1f, layerMask)))  /*&&  //1���� �÷����̸� ���� ����
                      !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask)*/  //�÷����� �ʹ� ������ �ö󰡱� ����� ������ ����
                {

                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                    Debug.Log("b");
                    Debug.Log(rb.velocity);
                }
                if ((Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) &&
                     Physics2D.OverlapCircle(wallCheck[1].position, 0.1f, layerMask))
                )
                {
                    Debug.Log("t1");
                    MonsterFlip();
                }


                Vector2 monsterFrontBelowPosition = (Vector2)transform.localPosition + new Vector2(-transform.localScale.x * 0.5f, 0f);

                Vector2 origin = monsterFrontBelowPosition;

                Vector2 direction = Vector2.down;

                float distance = 3f;

                // Raycast �ð������� ǥ��
                Debug.DrawRay(origin, direction * distance, Color.red);

                // Raycast�� ����Ͽ� ���� Ȯ��
                if (CheckisClif(origin, direction, distance, layerMask))
                {
                    Debug.Log("t2");

                    MonsterFlip();
                }

                if (canAtk && IsPlayerDir() && isGround)
                {
                    if (Vector2.Distance(transform.position, GameManager.Instance.GetPlayerPosition()) < 5f)
                    {
                        currentState = State.Attack;
                        break;
                    }
                }
            }
            yield return null;
        }
        if (currentState != State.Attack 
            /*&& currentState != State.Jump*/)
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
        if (!base.Hit && isGround)
        {
            //capsuleCollider.offset = capsuleColliderJumpOffset;
            canAtk = false;
            rb.velocity = new Vector2(-transform.localScale.x * 14f, jumpPower / 1.25f);
            MyAnimSetTrigger(currentState.ToString());

            yield return null;
            currentState = State.Idle;
        }
        else
        {
            currentState = State.Run;
        }

    }

    //IEnumerator Hit()
    //{
    //    yield return null;

    //    TakeDamage(1);

    //    yield return null;
    //    currentState = State.Idle;
    //}

    IEnumerator Die()
    {
        yield return null;



    }
    //IEnumerator Jump()
    //{
    //    yield return null;
    //    capsuleCollider.offset = capsuleColliderJumpOffset;

    //    rb.velocity = new Vector2(-transform.localScale.x * 6f, jumpPower);
    //    MyAnimSetTrigger("Attack");

    //    yield return Delay500;
    //    currentState = State.Idle;
    //}

    //void Update()
    //{
    //    GroundCheck();
    //    if (!isHit && isGround && !IsPlayingAnim("Run"))
    //    {
    //        capsuleCollider.offset = capsuleColliderOffset;
    //        MyAnimSetTrigger("Idle");
    //    }
    //}
}

