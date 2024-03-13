using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int health;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject player;
    public ObjectManager objectManager;

    SpriteRenderer spriteRenderer;
    Animator anim;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  
        
        if(enemyName == "B")
        {
            anim = GetComponent<Animator>();
        }
    }

    void OnEnable()
    {
        if(enemyName == "B")
        {
            health = 100;
            Invoke("Stop", 2);
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

        Invoke("Think", 2);
    }

    //패턴 케이스 로직
    void Think()
    {
        //현재 패턴이 패턴 갯수를 넘기면 0으로 돌아오는 로직
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch(patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;

        }
    }

    void FireForward()
    {
        //Debug.Log("앞으로 4발 발사.");
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.45f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        curPatternCount++;
        //패턴이 maxpattenrcount까지 가지 않았을 때 다시 실행
        if(curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireForward", 2);
        }
        else
        {
            Invoke("Think", 3);
        }
        
    }

    void FireShot()
    {
        //Debug.Log("플레이어 방향으로 샷건.");
        for(int index = 0; index < 5; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossA");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            //위치가 겹치지 않게 랜덤벡터를 더하여 구현
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f))
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        curPatternCount++;
        //패턴이 maxpattenrcount까지 가지 않았을 때 다시 실행
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireShot", 3.5f);
        }
        else
        {
            Invoke("Think", 3);
        }
    }

    void FireArc()
    {
        Debug.Log("부채모양으로 발사.");

        curPatternCount++;
        //패턴이 maxpattenrcount까지 가지 않았을 때 다시 실행
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArc", 0.15f);
        }
        else
        {
            Invoke("Think", 3);
        }
    }

    void FireAround()
    {
        Debug.Log("원 상태로 전체 공격.");

        curPatternCount++;
        //패턴이 maxpattenrcount까지 가지 않았을 때 다시 실행
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround", 0.7f);
        }
        else
        {
            Invoke("Think", 3);
        }
    }

    void Update()
    {
        if (enemyName == "B")
            return;
    }

    public void OnHit(int dmg)
    {
        if(health <= 0)
        {
            return;
        }
        health -= dmg;

        if(enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform" &&
           collision.gameObject.tag == "Ground" &&
           enemyName != "B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }

}
