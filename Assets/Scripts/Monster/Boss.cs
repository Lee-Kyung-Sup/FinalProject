using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            Invoke("Stop", 1);
        }
    }

    //������ ���� �����ϴ��� �Ǵ�
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

    //���� ���̽� ����
    void Think()
    {
        //���� ������ ���� ������ �ѱ�� 0���� ���ƿ��� ����
        //patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;

        if (health > 70)
        {
            patternIndex = 0; // ü���� 80 �̻��̸� ���� 0 ����
        }
        else if (health > 40)
        {
            patternIndex = 1; // ü���� 50 �̻��̸� ���� 1 ����
        }
        else if(health > 10)
        {
            patternIndex = 2; // ü���� 15�̻��̸� ���� 2 ����
        }
        else
        {
            patternIndex = 3; // ������ ���� ����
        }
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
        //Debug.Log("������ 4�� �߻�.");
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.1f;
        //GameObject bulletL = objectManager.MakeObj("BulletBossA");
        //bulletR.transform.position = transform.position + Vector3.right * 0.1f;
  

        Rigidbody2D rbR = bulletR.GetComponent<Rigidbody2D>();
        //Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
        
        Vector2 dirVec = player.transform.position - transform.position;

        rbR.AddForce(dirVec * 3, ForceMode2D.Impulse);
        //rbL.AddForce(dirVec * 3, ForceMode2D.Impulse);
      
        curPatternCount++;
        //������ maxpattenrcount���� ���� �ʾ��� �� �ٽ� ����
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
        //Debug.Log("�÷��̾� �������� ����.");
        for(int index = 0; index < 7; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossA");
            bullet.transform.position = transform.position;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            //��ġ�� ��ġ�� �ʰ� �������͸� ���Ͽ� ����
            Vector2 ranVec = new Vector2(Random.Range(-4f, 4f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rb.AddForce(dirVec.normalized * 6, ForceMode2D.Impulse);
        }

        curPatternCount++;
        //������ maxpattenrcount���� ���� �ʾ��� �� �ٽ� ����
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
        //Debug.Log("��ä������� �߻�.");
        GameObject bullet = objectManager.MakeObj("BulletBossA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //Vector2 dirVec = player.transform.position - transform.position;
        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI* 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        //dirVec += ranVec;
        rb.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;
        //������ maxpattenrcount���� ���� �ʾ��� �� �ٽ� ����
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
        //Debug.Log("�� ���·� ��ü ����.");
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
        //������ maxpattenrcount���� ���� �ʾ��� �� �ٽ� ����
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround", 0.7f);
        }
        else
        {
            Invoke("Think", 2);
        }
    }

    private Vector2 _targetPosition; // ��ǥ ��ġ
    public float moveSpeed = 3f; // �̵� �ӵ�
    void Start()
    {
        if(enemyName == "B")
        {
            InvokeRepeating("Patrol", 1f, 5f);
        }
    }

    void Patrol()
    {
        float randomX = Random.Range(-7f, 4f); // ������ x ��ǥ ����
        float randomY = Random.Range(-3f, 4f); // ������ y ��ǥ ����
        _targetPosition = new Vector2(randomX, randomY); // ������ ��ġ ���� ����
    }
    void Update()
    {
        if (enemyName == "B")
        {
            transform.position = Vector2.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
        }
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
