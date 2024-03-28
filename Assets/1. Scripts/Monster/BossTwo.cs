using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossTwo : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int currentHp;
    public Sprite[] sprites;

    public GameObject player;
    public ObjectManager objectManager;

    SpriteRenderer spriteRenderer;
    Animator anim;
    protected CircleCollider2D circleCollider;
    protected CapsuleCollider2D capsuleCollider;
    protected Rigidbody2D rb;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        objectManager = GameManager.instance.objectManager;
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        //transform.position = transform.position;    
    }
    void OnEnable()
    {
        if (enemyName == "BT")
        {
            currentHp = 100;
            Invoke("Stop", 1);
            //InvokeRepeating("Stop", 1, 1);
        }
    }

    //보스가 현재 존재하는지 판단
    void Stop()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        Invoke("Think", 1);
    }

    //패턴 케이스 로직
    void Think()
    {

        //if (currentHp > 70)
        //{
        //    patternIndex = 0; // 체력이 70 이상이면 패턴 0 실행
        //}
        //else if (currentHp > 40)
        //{
        //    patternIndex = 1; // 체력이 40 이상이면 패턴 1 실행
        //}
        //else if (currentHp > 10)
        //{
        //    patternIndex = 2; // 체력이 10이상이면 패턴 2 실행
        //}
        //else
        //{
        //    patternIndex = 3; // 나머지 패턴 실행
        //}


        //현재 패턴이 패턴 갯수를 넘기면 0으로 돌아오는 로직
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;

        curPatternCount = 0;
        anim.SetTrigger("Run");
        switch (patternIndex)
        {
            case 0:
                DragonFire();
                break;
            case 1:
                DragonAttack();
                break;
            case 2:
                DragonBurn();
                break;
            case 3:
                DragonRun();
                break;

        }

    }

    void DragonFire()
    {
        Debug.Log("DF");
        //드래곤이 불 오브젝트를 발사
        GameObject bulletD = objectManager.MakeObj("BulletBossBT");
        bulletD.transform.position = transform.position + Vector3.right * 13f;
        Rigidbody2D rb = bulletD.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.AddForce(transform.right * 5, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            anim.SetTrigger("Fire");
            Invoke("DragonFire", 3);
           
        }
        else
        {
            anim.SetTrigger("Run");
            Invoke("Think", 2);
        }
    }

    void DragonAttack()
    {
        Debug.Log("DA");
        //드래곤이 근접 공격
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(transform.position.x * 7f, 1.5f);
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            anim.SetTrigger("Attack");
            Invoke("DragonAttack", 3);

        }
        else
        {
            anim.SetTrigger("Run");
            Invoke("Think", 2);
        }
    }

    void DragonBurn()
    {
        Debug.Log("DB");
        //드래곤이 불길을 뿜음
    }

    void DragonRun()
    {
        Debug.Log("DR");
        //드래곤이 플레이어 가까이 다가왔다가 돌아감
    }

    public void EnableAttackCollider()
    {
        Debug.Log("true");
        capsuleCollider.enabled = true;
    }
    public void DisableAttackCollider()
    {
        Debug.Log("false");
        capsuleCollider.enabled = false;
    }

    public void Hit(int dmg)
    {
        if (currentHp <= 0)
        {
            return;
        }
        currentHp -= dmg;

        if (currentHp <= 0 && enemyName == "BT")
        {
            anim.SetTrigger("Die");
            Destroy(circleCollider);
            Destroy(capsuleCollider);

            Debug.Log("Monster Dead");

        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Ground" &&
        //   enemyName != "BT")

        //{
        //    gameObject.SetActive(false);
        //    transform.rotation = Quaternion.identity;
        //}
        if (collision.transform.tag == ("PlayerAttackBox"))
        {
            Hit(10); // 임시로 데미지 10함
        }
    }
}
