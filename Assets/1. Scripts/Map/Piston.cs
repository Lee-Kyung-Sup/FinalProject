using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : CharacterEnterTrigger
{
    [SerializeField]float upSpeed;
    [SerializeField]float downSpeed;
    [SerializeField]bool upDownCheck;
    Rigidbody2D rigi;
    List<Collision2D> Gfour = new List<Collision2D>(5);
    LayerMask fLayer;
    protected override void Awake()
    {
        base.Awake();
        rigi = GetComponent<Rigidbody2D>();
        fLayer = LayerMask.GetMask("Ground","Platform");
    }
    private void Update()
    {
        Move(); //데미지를 주는 조건 고려
        //이 오브젝트를 piston으로 적용후 Gfour에 들어간 객체들이 ground와 닿았을때 데미지
        //이 방법은 옆면에서도 판정이 있어서 주의해야함

        //이 피스톤이 느려진걸 모종의 방법으로 체크해서 데미지

        //Gfour에 들어간 객체들의 위아래 ground를 동시체크해서 데미지
        //들어간 객체들의 상태를 계속해서 체크할 방법을 가져야함

        //이 객체는 그저 위아래로만 움직일뿐
        //다른 객체들이 이것과 관계없이 위아래나 좌우 검사를해서 벽에 낑긴 상태가 되었을때 데미지
    }
    void Move()
    {
        if (upDownCheck)
        {
            rigi.velocity = Vector2.down * downSpeed;
            return;
        }
        rigi.velocity = Vector2.up * upSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (characterLayer.value == (characterLayer.value | (1 << collision.gameObject.layer)))//플랫폼 처럼 처리 고려
        {
            Gfour.Add(collision);
            return;
        }
        if (fLayer.value == (fLayer.value | (1 << collision.gameObject.layer)))
        {
            upDownCheck = !upDownCheck;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (characterLayer.value == (characterLayer.value | (1 << collision.gameObject.layer)))
        {
            Gfour.Remove(collision);
        }
    }
}
