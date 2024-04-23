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

    public Color hitColor = Color.red;
    public float hitDuration = 0.1f;

    private Color originalColor;
    private bool isHit = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = GameManager.Instance.Player;
        objectManager = ObjectManager.Instance;
        anim = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    void Update()
    {
        //transform.position = transform.position;
        if(isHit)
        {
            spriteRenderer.color = hitColor;
            Invoke("ResetColor", hitDuration);
            isHit = false;
        }

    }
    void OnEnable()
    {
        //Debug.Log("check");
            currentHp = 400;
           // Invoke("Stop", 1);
            //InvokeRepeating("Stop", 1, 1);
        
    }

    //������ ���� �����ϴ��� �Ǵ�
    public void Stop()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        //Invoke("Think", 2);
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
       // patternIndex = patternIndex == 4 ? 0 : patternIndex + 1;
       // Debug.Log(patternIndex);
       // curPatternCount = 0;
       //// anim.SetTrigger("Run");
       // switch (patternIndex)
       // {
       //     case 0:
       //         anim.SetTrigger("Run");
       //         break;
       //     case 1:
       //         DragonFire();

       //         break;
       //     case 2:
       //         DragonAttack();
       //         break;
       //     case 3:
       //         DragonBurn();
       //         break;
       //     case 4:
       //         DragonRunAttack();
       //         break;

       // }

    }

    void DragonFire()
    {
        
        Debug.Log("DF");
        //�巡���� �� ������Ʈ�� �߻�
        GameObject bulletD = objectManager.MakeObj("BulletBossBT");
        bulletD.transform.position = transform.position + new Vector3(9f, 2f, 0);
        Rigidbody2D rb = bulletD.GetComponent<Rigidbody2D>();
        
        rb.AddForce(transform.right * 6, ForceMode2D.Impulse);
        
        //curPatternCount++;
        
        //if (curPatternCount < maxPatternCount[patternIndex])
        //{
        //    anim.SetTrigger("Fire");
        //    Invoke("DragonFire", 3);
           
        //}
        //else
        //{
        //    CancelInvoke("Think"); 
        //    Invoke("Think", 2);
        //}
    }

    void DragonAttackF()
    {
        Debug.Log("DA");
        //�巡���� ���� ����
        
        rb.velocity = new Vector2(15f, 2f);
        //    anim.SetTrigger("Attack");
        //curPatternCount++;

        //if (curPatternCount < maxPatternCount[patternIndex])
        //{

        //    rb.velocity = new Vector2(transform.localScale.x * 7f, 1.5f);
        //    anim.SetTrigger("Attack");
        //    Invoke("DragonAttack", 2);

        //}
        //else
        //{
        //    rb.velocity = new Vector2(transform.localScale.x * -7f, 1.5f);
        //    CancelInvoke("Think");
        //    Invoke("Think", 2);


        //}
    }

    void DragonAttackB()
    {
       
        rb.velocity = new Vector2(-15f, 2f);
    }

    void DragonBurn()
    {
        rb.velocity = Vector2.zero;
        Debug.Log("DB");
        //�巡���� �ұ��� ����
        for (int index = 0; index < 3; index++)
        {     

            GameObject bulletD = objectManager.MakeObj("BulletBossBT");
            //bulletD.transform.position = transform.position;
            Rigidbody2D rba = bulletD.GetComponent<Rigidbody2D>();
            

            //rb.gravityScale = 0f;
            bulletD.transform.position = transform.position + new Vector3(7f, 4f, 0);
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-1f, 1f), Random.Range(-10f, 10f));
            dirVec += ranVec;
            rba.AddForce(dirVec.normalized * 6, ForceMode2D.Impulse);
        }
        
        //curPatternCount++;
        ////������ maxpattenrcount���� ���� �ʾ��� �� �ٽ� ����
        //if (curPatternCount < maxPatternCount[patternIndex])
        //{
        //    anim.SetTrigger("Burn");
        //    Invoke("DragonBurn", 2f);
        //}
        //else
        //{
        //    CancelInvoke("Think");
        //    Invoke("Think", 2);
        //}
    }

    //private Vector2 _targetPosition;
    //public float moveSpeed = 3f;
    //public bool isPatrolling = false;
    void DragonRunAttack()
    {
        Debug.Log("DR");
        //�巡���� �÷��̾� ������ �ٰ��Դٰ� ���ư�
       
        //curPatternCount++;
        ////������ maxpattenrcount���� ���� �ʾ��� �� �ٽ� ����
        //if (curPatternCount < maxPatternCount[patternIndex])
        //{
        //    anim.SetTrigger("RunAttack");
        //    if (!isPatrolling)
        //    {
        //        isPatrolling = true;
        //        Invoke("Patrol", 2f);
        //        Invoke("returnInitialPosition",2f);
        //    }
        //}
        //else
        //{
        //    CancelInvoke("Patrol");
        //    CancelInvoke("Think");
        //    Invoke("Think", 2);
        //}

    }

    //public void Patrol()
    //{
    //    float randomX = Random.Range(0f, 7f);
    //    _targetPosition = new Vector2(randomX, 0);
    //    transform.position = Vector2.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
    //    //Invoke("DragonRunAttack", 3f);
    //}

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

    //public void returnInitialPosition()
    //{
    //    float distance = Vector2.Distance(transform.position, initialPosition);
    //    rb.velocity = (initialPosition - (Vector2)transform.position).normalized * distance * 1f;
       
    //}
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
        //else
        //{
        //    anim.SetTrigger("Hit");
        //}
    }


    public void Destroy()
    {
        
        Destroy(gameObject);
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
            isHit= true;
        }
    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }
}
