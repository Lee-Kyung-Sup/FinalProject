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

        //if (currentHp > 70)
        //{
        //    patternIndex = 0; // ü���� 70 �̻��̸� ���� 0 ����
        //}
        //else if (currentHp > 40)
        //{
        //    patternIndex = 1; // ü���� 40 �̻��̸� ���� 1 ����
        //}
        //else if (currentHp > 10)
        //{
        //    patternIndex = 2; // ü���� 10�̻��̸� ���� 2 ����
        //}
        //else
        //{
        //    patternIndex = 3; // ������ ���� ����
        //}


        //���� ������ ���� ������ �ѱ�� 0���� ���ƿ��� ����
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;

        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                anim.SetTrigger("Fire");
                //DragonFire();
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
        //�巡���� �� ������Ʈ�� �߻�
        GameObject bulletD = objectManager.MakeObj("BulletBossBT");
        bulletD.transform.position = transform.position + Vector3.right * 13f;
        Rigidbody2D rb = bulletD.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.AddForce(transform.right * 5, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
           
            Invoke("DragonFire", 3);
           
        }
        else
        {
            Invoke("Think", 2);
        }
    }

    void DragonAttack()
    {
        Debug.Log("DA");
        //�巡���� ���� ����
    }

    void DragonBurn()
    {
        Debug.Log("DB");
        //�巡���� �ұ��� ����
    }

    void DragonRun()
    {
        Debug.Log("DR");
        //�巡���� �÷��̾� ������ �ٰ��Դٰ� ���ư�
    }
}
