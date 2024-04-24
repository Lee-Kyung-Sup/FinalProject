using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
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
    public float bulletLifetime = 2f;
    public ObjectManager objectManager;
    public GameObject player;
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

    protected void Start()
    {
        player = GameManager.Instance.Player;
        objectManager = ObjectManager.Instance;
    }
    //바닥에 닿았는지 아닌지 체크
    protected void GroundCheck()
    {
        Debug.DrawRay(new Vector2(transform.localPosition.x, transform.localPosition.y + 0.2f), Vector2.down * 3f, Color.red);
        if (Physics2D.Raycast(new Vector2(transform.localPosition.x, transform.localPosition.y + 0.2f), Vector2.down, 3f, layerMask))
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
        while (currentHp >= 0)
        {
            yield return StartCoroutine(currentState.ToString());
        }
        StopAllCoroutines();
    }

    IEnumerator Idle()
    {
        yield return null;

        MyAnimSetTrigger(currentState.ToString());
        //if (Random.value > 0.5f)
        //{
        //    //MonsterFlip();
        //}
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


                Vector2 monsterFrontBelowPosition = (Vector2)transform.localPosition + new Vector2(-transform.localScale.x * 0.5f, 0.3f);

                Vector2 origin = monsterFrontBelowPosition;

                Vector2 direction = Vector2.down;

                float distance = 3f;

                // Raycast 시각적으로 표시
                Debug.DrawRay(origin, direction * distance, Color.red);

                // Raycast를 사용하여 조건 확인
                if (CheckisClif(origin, direction, distance, layerMask))
                {
                    Debug.Log("t2");

                    MonsterFlip();
                }
                if (canAtk && IsPlayerDir() && isGround)
                {
                    if (Vector2.Distance(transform.position, GameManager.Instance.GetPlayerPosition()) < 15f)
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

    //private List<GameObject> frogbullet = new List<GameObject>();
    public void Fire()
    {
        //GameObject bulletClone = ObjectManager.Instance.MakeObj("FrogBullet"); 
        //if (bulletClone != null)
        //{
        //    bulletClone.transform.position = collision.transform.position;
        //    bulletClone.transform.rotation = Quaternion.identity;
        //}

        GameObject bulletClone = objectManager.MakeObj("frogBullet");
        bulletClone.transform.position = transform.position + Vector3.right * 1f;

        Rigidbody2D rbf = bulletClone.GetComponent<Rigidbody2D>();

        Vector2 dirVec = player.transform.position - transform.position;
        rbf.AddForce(dirVec * 3, ForceMode2D.Impulse);
        //    GameObject bulletClone = Instantiate(bullet, genPoint.position, transform.rotation);

        //    bulletClone.GetComponent<Rigidbody2D>().velocity = transform.right * -transform.localScale.x * 10f;
        //    bulletClone.transform.localScale = new Vector2(transform.localScale.x, 1f);
        //    frogbullet.Add(bulletClone);

        //    StartCoroutine(DestroyBulletAfterTime(bulletClone));
        //}

        //IEnumerator DestroyBulletAfterTime(GameObject bullet)
        //{
        //    yield return new WaitForSeconds(bulletLifetime);
        //    Destroy(bullet);
        //    frogbullet.Remove(bullet);
        //}

        //protected override void OnTriggerEnter2D(Collider2D collision)
        //{
        //    base.OnTriggerEnter2D(collision);
        //    if (collision.transform.tag == ("Player"))
        //    {
        //        Destroy(bullet);
        //    }
        //}
        ////몬스터 데미지 받기
        //public override void TakeDamage(int dam)
        //{
        //    currentHp -= dam;
        //    isHit = true;
        //    MyAnimSetTrigger("Hit");
        //    //() 죽거나 넉백일경우 코드구현하기
        //    hitBoxCollider.SetActive(false);
        //}
    }
}

