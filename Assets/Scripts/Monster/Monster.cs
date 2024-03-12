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


    //hitboxxcollider가 비활성화시 잠시 후에 다시 활성
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

    //공격가능쿨타임
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

    //실행중인 애니메이션이 특정이름과 일치하는지 확인
    public bool IsPlayingAnim(string AnimName)
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName))
        {
            return true;
        }
        return false;
    }

    //중복된 애니메이션 실행 방지
    public void MyAnimSetTrigger(string AnimName)
    {
        if (!IsPlayingAnim(AnimName))
        {
            Anim.SetTrigger(AnimName);
        }
    }

    //몬스터 방향전환 true이면 오른쪽, false이면 원쪽
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

    //플레이어 위치값 받아와서 몬스터 방향조정
    protected bool IsPlayerDir()
    {
        if (transform.position.x < GameManager.instance.GetPlayerPosition().x ? MonsterDirRight : !MonsterDirRight)
        {
            return true;
        }
        return false;
    }

    //바닥에 닿았는지 아닌지 체크
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

    //몬스터 데미지 받기
    public void TakeDamage(int dam)
    {
        currentHp -= dam;
        isHit = true;
        //() 죽거나 넉백일경우 코드구현하기
        hitBoxCollider.SetActive(false);
    }

    //특정태그에 따른 충돌 피해
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if ( collision.transform.CompareTag ( ?? ) )
        //{
        //TakeDamage ( 0 );
        //}
    }

  
}