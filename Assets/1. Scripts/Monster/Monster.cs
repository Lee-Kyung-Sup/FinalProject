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
                Hit = false;
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

    public void Destroy()
    {
        Destroy(gameObject);
    }

    //실행중인 애니메이션이 특정이름과 일치하는지 확인
    public bool IsPlayingAnim(string AnimName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName))
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
            anim.SetTrigger(AnimName);
        }
    }

    //몬스터 방향전환 true이면 오른쪽, false이면 원쪽
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

    //플레이어 위치값 받아와서 몬스터 방향조정
    protected bool IsPlayerDir()
    {
        if (transform.position.x < GameManager.Instance.GetPlayerPosition().x ? monsterDirRight : !monsterDirRight)
        {
            return true;
        }
        return false;
    }

    //벽체크
    public bool CheckisClif(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask)
    {
        // Raycast 발사
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, layerMask);

        // Raycast가 벽에 부딪히면 false 반환
        if (hit.collider != null)
        {
            return false;
        }

        return true;
    }

    //몬스터 데미지 받기
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

    //특정태그에 따른 충돌 피해

    protected virtual void OnTriggerEnter2D(Collider2D collision) // 트리거로 데미지 감지
    {
        if (((1 << collision.gameObject.layer) & (1 << 19) | (1 << 20)) != 0) // 19 : 플레이어 어택박스 레이어 , 20: 플레이어 불렛
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