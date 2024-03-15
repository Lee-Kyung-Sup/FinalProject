using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : CharacterEnterTrigger
{
    float upSpeed;
    float downSpeed;
    Rigidbody2D rigi;
    List<Collider2D> Gfour = new List<Collider2D>(5);
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
            foreach (Collider2D item in Gfour)
            {
                //최대 체력을 쉽게 찾을 방법? 인터페이스로 똑같이?
                //  item.getcom<dmg>().GiveDmg( 최대 체력 비례 );
            }
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
