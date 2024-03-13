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
        //Debug.Log("������ 4�� �߻�.");
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.6f;
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.left * 0.6f;

        Rigidbody2D rbR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rbRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rbLL = bulletLL.GetComponent<Rigidbody2D>();

        rbR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rbRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rbL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rbLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
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
        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI* 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rb.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;
        //������ maxpattenrcount���� ���� �ʾ��� �� �ٽ� ����
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
        Debug.Log("�� ���·� ��ü ����.");

        curPatternCount++;
        //������ maxpattenrcount���� ���� �ʾ��� �� �ٽ� ����
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