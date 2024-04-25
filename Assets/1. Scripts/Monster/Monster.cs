using System.Collections;

using UnityEngine;


public class Monster : MonoBehaviour, IsGroundable, IDamageable
{
    public int currentHp = 1;
    public float moveSpeed = 5f;
    public float jumpPower = 10;
    public float atkCoolTime = 3f;
    public float atkCoolTimeCalc = 3f;

    public bool Hit = false;
    public bool isGround = true;
    public bool canAtk = true;
    public bool monsterDirRight;
    public bool monsterIdle = true;

    protected Rigidbody2D rb;
    protected CapsuleCollider2D capsuleCollider;
    public GameObject hitBoxCollider;
    public Animator anim;
    protected LayerMask layerMask;

    


    protected virtual void Awake()
    {
        layerMask = LayerMask.GetMask("Ground","Platform");
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        StartCoroutine(CalcCoolTime());
        StartCoroutine(ResetCollider());
    }


    //hitboxxcollider�� ��Ȱ��ȭ�� ��� �Ŀ� �ٽ� Ȱ��
    IEnumerator ResetCollider()
    {
        while (true)
        {
            yield return null;
            if (!hitBoxCollider.activeInHierarchy)
            {
                yield return new WaitForSeconds(0.5f);
                hitBoxCollider.SetActive(true);
                Hit = false;
            }
        }
    }

    //���ݰ�����Ÿ��
    IEnumerator CalcCoolTime()
    {
        while (true)
        {
            yield return null;
            if (!canAtk)
            {
                atkCoolTimeCalc -= Time.deltaTime;
                if (atkCoolTimeCalc <= 0)
                {
                    atkCoolTimeCalc = atkCoolTime;
                    canAtk = true;
                }
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    //�������� �ִϸ��̼��� Ư���̸��� ��ġ�ϴ��� Ȯ��
    public bool IsPlayingAnim(string AnimName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName))
        {
            return true;
        }
        return false;
    }

    //�ߺ��� �ִϸ��̼� ���� ����
    public void MyAnimSetTrigger(string AnimName)
    {
        if (!IsPlayingAnim(AnimName))
        {
            anim.SetTrigger(AnimName);
        }
    }

    //���� ������ȯ true�̸� ������, false�̸� ����
    protected void MonsterFlip()
    {
        if(isGround == false)
        {
            return;
        }
        monsterDirRight = !monsterDirRight;
        
        Vector3 thisScale = transform.localScale;
        if (monsterDirRight)
        {
            thisScale.x = -Mathf.Abs(thisScale.x);
            
        }
        else
        {
            thisScale.x = Mathf.Abs(thisScale.x);
            
        }
        transform.localScale = thisScale;
        rb.velocity = Vector2.zero;

    }

    //�÷��̾� ��ġ�� �޾ƿͼ� ���� ��������
    protected bool IsPlayerDir()
    {
        if (transform.position.x < GameManager.Instance.GetPlayerPosition().x ? monsterDirRight : !monsterDirRight)
        {
            return true;
        }
        return false;
    }

    //��üũ
    public bool CheckisClif(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask)
    {
        // Raycast �߻�
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, layerMask);

        // Raycast�� ���� �ε����� false ��ȯ
        if (hit.collider != null)
        {
            return false;
        }

        return true;
    }

    //���� ������ �ޱ�
    public virtual void TakeDamage(int dam)
    {
        if(currentHp <= 0)
        {
            return;
        }
        currentHp -= dam;
        Hit = true;

        if(currentHp <= 0)
        {
            MyAnimSetTrigger("Die");
            Destroy(capsuleCollider);
            Destroy(hitBoxCollider);
            Destroy(rb);
            Debug.Log("Monster Dead");
            StopAllCoroutines();
        }
        else
        {
            MyAnimSetTrigger("Hit");
            rb.velocity = Vector2.zero;
            if (transform.position.x > GameManager.Instance.GetPlayerPosition().x) 
            {
                Debug.Log("D");
                rb.velocity = new Vector2(10f, 0);
                
            }
            else
            {
                Debug.Log("E");
                rb.velocity = new Vector2(-10f, 0);
                
            }
        }
        Debug.Log("F");
        //MyAnimSetTrigger("Move");
        hitBoxCollider.SetActive(false);
    }

    //Ư���±׿� ���� �浹 ����

    protected virtual void OnTriggerEnter2D(Collider2D collision) // Ʈ���ŷ� ������ ����
    {
        if (((1 << collision.gameObject.layer) & (1 << 19) | (1 << 20)) != 0) // 19 : �÷��̾� ���ùڽ� ���̾� , 20: �÷��̾� �ҷ�
        {
            DeflectBullet deflectBullet = collision.GetComponent<DeflectBullet>();
            if (deflectBullet != null)
            {
                TakeDamage(deflectBullet.damage);
            }
        }
    }

    public bool IsGround()
    {
        return isGround;
    }
}