using System.Collections;
using Unity.VisualScripting;
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

    WaitForSeconds Delay5000 = new WaitForSeconds(5f);

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

    //�ٴڿ� ��Ҵ��� �ƴ��� üũ
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
    void Update()
    {
        GroundCheck();
    }
    IEnumerator FSM()
    {
        while (currentHp >=0)
        {
            yield return StartCoroutine(currentState.ToString());
        }
        StopAllCoroutines();
    }

    IEnumerator Idle()
    {
        yield return null;
        
        MyAnimSetTrigger(currentState.ToString());
        if (Random.value > 0.5f)
        {
            //MonsterFlip();
        }
        yield return Delay5000;
        currentState = State.Run;
      
    }

        IEnumerator Run()
    {
        yield return null;
        float runTime = Random.Range(2f, 3f);
        
        while (runTime > 0)
        {
            MyAnimSetTrigger(currentState.ToString());
            runTime -= Time.deltaTime;
            
            if (!base.Hit)
            {
                rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);

                if ((!Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) &&  //��üũ 0�� �÷����̾���
                      Physics2D.OverlapCircle(wallCheck[1].position, 0.1f, layerMask)) /*&&  //1���� �÷����̸� ���� ����
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
            if (Random.value < 0.5f)
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
        MyAnimSetTrigger(currentState.ToString());
        

        yield return null;
        currentState = State.Idle;
    }

    IEnumerator Hit()
    {
        yield return null;

        TakeDamage(1);
       
        yield return null;
        currentState = State.Idle;
    }

    IEnumerator Die()
    {
        yield return null;

        

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

    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    base.OnTriggerEnter2D(collision);
    //    if (collision.transform.tag == ("Player"))
    //    {
    //        Destroy(bullet);
    //    }
    //}
    ////���� ������ �ޱ�
    //public override void TakeDamage(int dam)
    //{
    //    currentHp -= dam;
    //    isHit = true;
    //    MyAnimSetTrigger("Hit");
    //    //() �װų� �˹��ϰ�� �ڵ屸���ϱ�
    //    hitBoxCollider.SetActive(false);
    //}
}

