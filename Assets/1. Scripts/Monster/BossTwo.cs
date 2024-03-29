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
    protected Vector2 initialPosition;

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
        initialPosition = transform.position;
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

        Invoke("Think", 2);
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
       // anim.SetTrigger("Run");
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
                DragonRunAttack();
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
            anim.SetTrigger("Fire");
            //Invoke("DragonFire", 5);
           
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
        //�巡���� ���� ����
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(transform.localScale.x * 7f, 1.5f);
       
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(transform.localScale.x * 7f, 1.5f);
            anim.SetTrigger("Attack");
            //Invoke("DragonAttack", 2);
            
        }
        else
        {
            rb.velocity = new Vector2(transform.localScale.x * -7f, 1.5f);
            anim.SetTrigger("Run");
            Invoke("Think", 2);
            

        }
    }

    void DragonBurn()
    {
        Debug.Log("DB");
        //�巡���� �ұ��� ����
        for (int index = 0; index < 5; index++)
        {
            GameObject bulletD = objectManager.MakeObj("BulletBossBT");
            Rigidbody2D rb = bulletD.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            bulletD.transform.position = transform.position + Vector3.right * 7f;
           // Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(0f, 1f), Random.Range(-5f, 5f));
            //dirVec += ranVec;
            rb.AddForce(ranVec.normalized * 6, ForceMode2D.Impulse);
        }
            
        curPatternCount++;
        //������ maxpattenrcount���� ���� �ʾ��� �� �ٽ� ����
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            anim.SetTrigger("Burn");
            //Invoke("DragonBurn", 1f);
        }
        else
        {
            anim.SetTrigger("Run");
            Invoke("Think", 2);
        }
    }

    private Vector2 _targetPosition;
    public float moveSpeed = 3f;
    void DragonRunAttack()
    {
        Debug.Log("DR");
        //�巡���� �÷��̾� ������ �ٰ��Դٰ� ���ư�
        //float randomX = Random.Range(-7f, 4f);
        //_targetPosition = new Vector2(randomX, 0);
        //transform.position = Vector2.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
        curPatternCount++;
        //������ maxpattenrcount���� ���� �ʾ��� �� �ٽ� ����
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            anim.SetTrigger("RunAttack");
            InvokeRepeating("Patrol", 1f, 4f);
        }
        else
        {
            anim.SetTrigger("Run");
            Invoke("Think", 2);
        }



    }

    public void Patrol()
    {
        float randomX = Random.Range(0f, 5f);
        _targetPosition = new Vector2(randomX, 0);
        transform.position = Vector2.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
        Invoke("DragonRunAttack", 3f);
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

    public void returnInitialPosition()
    {
        float distance = Vector2.Distance(transform.position, initialPosition);
        rb.velocity = (initialPosition - (Vector2)transform.position).normalized * distance * 1f;
       
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
        //    //transform.rotation = Quaternion.identity;
        //}
       
        if (collision.transform.tag == ("PlayerAttackBox"))
        {
            Hit(10); // �ӽ÷� ������ 10��
        }
    }
}
