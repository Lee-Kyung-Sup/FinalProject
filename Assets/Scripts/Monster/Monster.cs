using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int currentHp = 1;
    public float moveSpeed = 5f;
    public float jumpPower = 10;
    public float atkCoolTime = 3f;
    public float atkCoolTimeCalc = 3f;

    public bool isHit = false;
    public bool isGround = true;
    public bool canAtk = true;
    public bool MonsterDirRight;

    protected Rigidbody2D rb;
    protected CapsuleCollider2D capsuleCollider;
    public GameObject hitBoxCollider;
    public Animator Anim;
    public LayerMask layerMask;




    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Anim = GetComponent<Animator>();

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
                isHit = false;
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

    //�������� �ִϸ��̼��� Ư���̸��� ��ġ�ϴ��� Ȯ��
    public bool IsPlayingAnim(string AnimName)
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName))
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
            Anim.SetTrigger(AnimName);
        }
    }

    //���� ������ȯ true�̸� ������, false�̸� ����
    protected void MonsterFlip()
    {
        MonsterDirRight = !MonsterDirRight;

        Vector3 thisScale = transform.localScale;
        if (MonsterDirRight)
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
        if (transform.position.x < GameManager.instance.GetPlayerPosition().x ? MonsterDirRight : !MonsterDirRight)
        {
            return true;
        }
        return false;
    }

    //�ٴڿ� ��Ҵ��� �ƴ��� üũ
    protected void GroundCheck()
    {
        if (Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.size, capsuleCollider.direction,
            0, Vector2.down, 0.05f, layerMask))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    //���� ������ �ޱ�
    public void TakeDamage(int dam)
    {
        currentHp -= dam;
        isHit = true;
        //() �װų� �˹��ϰ�� �ڵ屸���ϱ�
        hitBoxCollider.SetActive(false);
    }

    //Ư���±׿� ���� �浹 ����
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if ( collision.transform.CompareTag ( ?? ) )
        //{
        //TakeDamage ( 0 );
        //}
    }

  
}