using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    public string enemyName;
    public float speed;
    public int currentHp;
    public Sprite[] sprites;
    public bool bossDir;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject player;
    public ObjectManager objectManager;

    SpriteRenderer spriteRenderer;
    Animator anim;
    protected CircleCollider2D circleCollider;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    void OnEnable()
    {
        if(enemyName == "B")
        {
            currentHp = 200;
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
       
        //현재 패턴이 패턴 갯수를 넘기면 0으로 돌아오는 로직
        //patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        
        
            if (currentHp > 150)
            {
                patternIndex = 0; // 체력이 70 이상이면 패턴 0 실행
            }
            else if (currentHp >100)
            {
                patternIndex = 1; // 체력이 40 이상이면 패턴 1 실행
            }
            else if (currentHp > 50)
            {
                patternIndex = 2; // 체력이 10이상이면 패턴 2 실행
            }
            else
            {
                patternIndex = 3; // 나머지 패턴 실행
            }
            curPatternCount = 0;

            switch (patternIndex)
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
        bulletR.transform.position = transform.position + Vector3.right * 0.1f;
        //GameObject bulletL = objectManager.MakeObj("BulletBossA");
        //bulletR.transform.position = transform.position + Vector3.right * 0.1f;
  

        Rigidbody2D rbR = bulletR.GetComponent<Rigidbody2D>();
        //Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
        
        Vector2 dirVec = player.transform.position - transform.position;

        rbR.AddForce(dirVec * 2, ForceMode2D.Impulse);
        //rbL.AddForce(dirVec * 3, ForceMode2D.Impulse);
      
        curPatternCount++;
        //패턴이 maxpattenrcount까지 가지 않았을 때 다시 실행
        if(curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireForward", 1);
        }
        else
        {
            Invoke("Think", 2);
        }
        
    }

    void FireShot()
    {
        //Debug.Log("플레이어 방향으로 샷건.");
        for(int index = 0; index < 7; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossA");
            bullet.transform.position = transform.position;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            //위치가 겹치지 않게 랜덤벡터를 더하여 구현
            Vector2 ranVec = new Vector2(Random.Range(-4f, 4f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rb.AddForce(dirVec.normalized * 6, ForceMode2D.Impulse);
        }

        curPatternCount++;
        //패턴이 maxpattenrcount까지 가지 않았을 때 다시 실행
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireShot", 2f);
        }
        else
        {
            Invoke("Think", 2);
        }
    }

    void FireArc()
    {
        //Debug.Log("부채모양으로 발사.");
        GameObject bullet = objectManager.MakeObj("BulletBossA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //Vector2 dirVec = player.transform.position - transform.position;
        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI* 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        //dirVec += ranVec;
        rb.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;
        //패턴이 maxpattenrcount까지 가지 않았을 때 다시 실행
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArc", 0.15f);
        }
        else
        {
            Invoke("Think", 2);
        }
    }

    void FireAround()
    {
        //Debug.Log("원 상태로 전체 공격.");
        int roundNumA = 30;
        int roundNumB = 20;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;
        
        for(int index=0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossA");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 2 * index/roundNum), Mathf.Cos(Mathf.PI * 2 * index / roundNum));
            rb.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);
        }
       
        curPatternCount++;
        //패턴이 maxpattenrcount까지 가지 않았을 때 다시 실행
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround", 0.7f);
        }
        else
        {
            Invoke("Think", 2);
        }
    }

    private Vector2 _targetPosition; // 목표 위치
    public float moveSpeed = 3f; // 이동 속도
    void Start()
    {
        //AudioManager.Instance.PlayBGM("FirstBoss"); //원래 BGM 재생 JHP
        if (enemyName == "B")
        {
            InvokeRepeating("Patrol", 1f, 2f);
        }
        player = GameManager.Instance.Player;
        objectManager = ObjectManager.Instance;
        
        if (enemyName == "B")
        {
            anim = GetComponent<Animator>();
        }
    }

    void Patrol()
    {
        float randomX = Random.Range(-5f, 7f); // 랜덤한 x 좌표 생성
        float randomY = Random.Range(-2f, 7f); // 랜덤한 y 좌표 생성
        _targetPosition = new Vector2(randomX, randomY); // 랜덤한 위치 벡터 생성
        
    }
    void Update()
    {
        if (transform.position.x < GameManager.Instance.GetPlayerPosition().x ? bossDir : !bossDir)
        {
            
            Vector3 thisScale = transform.localScale;
            //Debug.Log(bossDir);
            if (bossDir)
            {
                //Debug.Log("b");
                thisScale.x = -Mathf.Abs(thisScale.x);

            }
            
            else
            {
                thisScale.x = Mathf.Abs(thisScale.x);

            }
            transform.localScale = thisScale;
        }
        else
        {
            Vector3 thisScale = transform.localScale;
            //Debug.Log(bossDir);
            if (!bossDir)
            {
                //Debug.Log("b");
                thisScale.x = -Mathf.Abs(thisScale.x);

            }

            else
            {
                thisScale.x = Mathf.Abs(thisScale.x);

            }
            transform.localScale = thisScale;
        }
        if (enemyName == "B")
        {
            transform.position = Vector2.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
        }
            return;

    }

    public void Destroy()
    {
        //AudioManager.Instance.StopBGM(); // 기존 BGM 정지 JHP
        //AudioManager.Instance.PlayBGM("FirstChapter"); //원래 BGM 재생 JHP
        Destroy(gameObject);
    }

    public void TakeDamage(int dmg)
    {
        if(currentHp <= 0)
        {
            return;
        }
        Debug.Log($"받은 데미지 : {dmg}");
        currentHp -= dmg;

        if (currentHp <= 0 && enemyName =="B")
        {
            anim.SetTrigger("Die");
            Destroy(circleCollider);
            
            Debug.Log("Monster Dead");
            
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" &&
           enemyName != "B")

        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        if (((1 << collision.gameObject.layer) | (1 << 19) |(1 << 20)) != 0) // 19 : 플레이어 어택박스 레이어 , 20: 플레이어 불렛
        {
            DeflectBullet deflectBullet = collision.GetComponent<DeflectBullet>();
            if (deflectBullet != null)
            {
                TakeDamage(deflectBullet.damage);
            }
        }
    }

    //void BossDir()
    //{
    //    if (transform.position.x < GameManager.instance.GetPlayerPosition().x ? bossDir : !bossDir)
    //    {
    //        Vector3 thisScale = transform.localScale;
    //        if (bossDir)
    //        {
    //            thisScale.x = -Mathf.Abs(thisScale.x);

    //        }
    //        else
    //        {
    //            thisScale.x = Mathf.Abs(thisScale.x);

    //        }
    //        transform.localScale = thisScale;
    //    }
       
    //}
}
