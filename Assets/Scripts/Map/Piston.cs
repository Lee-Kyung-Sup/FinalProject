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
    protected override void Awake()
    {
        base.Awake();
        rigi = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move(); //데미지를 주는 조건 고려
        //이 오브젝트를 piston으로 적용후 Gfour에 들어간 객체들이 ground와 닿았을때 데미지
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
        }
        if (LayerMask.GetMask("Ground") == (LayerMask.GetMask("Ground") | (1 << collision.gameObject.layer)))
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
