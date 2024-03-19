using System.Collections;
using UnityEngine;

public class Frog : Monster
{
    public enum State
    {
        Idle,
        Run,
        Attack,
    }

    public State currentState = State.Idle;

    public Transform[] wallCheck;
    public Transform genPoint;
    public GameObject bullet;
    public float bulletLifetime = 3f;

    WaitForSeconds Delay1000 = new WaitForSeconds(1f);

    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 1f;
        jumpPower = 15f;
        currentHp = 4;
        atkCoolTime = 3f;
        atkCoolTimeCalc = atkCoolTime;

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
        yield return null;
        MyAnimSetTrigger("Idle");

        if (Random.value > 0.5f)
        {
            MonsterFlip();
        }
        yield return Delay1000;
        currentState = State.Run;
    }

    IEnumerator Run()
    {
        yield return null;
        float runTime = Random.Range(2f, 3f);
        while (runTime > 0)
        {
            runTime -= Time.deltaTime;
            MyAnimSetTrigger("Run");
            if (!isHit)
            {
                rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);

                if ((!Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) &&  //벽체크 0번 플랫폼이없고
                      Physics2D.OverlapCircle(wallCheck[1].position, 0.1f, layerMask)) /*&&  //1번이 플랫폼이면 몬스터 점프
                       !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask)*/)  //플랫폼과 너무 가까우면 올라가기 힘들기 때문에 넣음
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


                Vector2 monsterFrontBelowPosition = (Vector2)transform.localPosition + new Vector2(-transform.localScale.x * 0.2f, -1f);

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
                if (canAtk && IsPlayerDir())
                {
                    if (Vector2.Distance(transform.position, GameManager.instance.GetPlayerPosition()) < 15f)
                    {
                        currentState = State.Attack;
                        break;
                    }
                }


            }
            yield return null;
        }

        if (currentState != State.Attack)
        {
            if (Random.value > 0.5f)
            {
                MonsterFlip();
            }
            else
            {
                currentState = State.Idle;
            }
        }
    }

    IEnumerator Attack()
    {
        yield return null;

        canAtk = false;
        rb.velocity = new Vector2(0, jumpPower);
        MyAnimSetTrigger("Attack");

        yield return Delay1000;
        currentState = State.Idle;
    }

    public void Fire()
    {
        GameObject bulletClone = Instantiate(bullet, genPoint.position, transform.rotation);
        bulletClone.GetComponent<Rigidbody2D>().velocity = transform.right * -transform.localScale.x * 10f;
        bulletClone.transform.localScale = new Vector2(transform.localScale.x, 1f);

        StartCoroutine(DestroyBulletAfterTime(bulletClone));
    }

    IEnumerator DestroyBulletAfterTime(GameObject bullet)
    {
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(bullet);
    }

}

