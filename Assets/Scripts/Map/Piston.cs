using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : CharacterEnterTrigger
{
    float upSpeed;
    float downSpeed;
    Rigidbody2D rigi;
    List<Collider2D> Gfour = new List<Collider2D>();
    protected override void Awake()
    {
        base.Awake();
        rigi = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //move
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (characterLayer.value == (characterLayer.value | (1 << collision.gameObject.layer)))
        {
            Gfour.Add(collision);
        }
        if (LayerMask.GetMask("Ground") == (LayerMask.GetMask("Ground") | (1 << collision.gameObject.layer)))
        {
            // 최대 체력을 또한 인터페이스를 쓰는 수 밖에 없는건가? 다른 방법은?

            //Gfour.getcom<Interface>().dmg(// maxhp * (3 / 5));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (characterLayer.value == (characterLayer.value | (1 << collision.gameObject.layer)))
        {
            Gfour.Remove(collision);
        }
    }
}
