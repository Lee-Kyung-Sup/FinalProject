using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : PlayerEnterTrigger
{
    Rigidbody2D pRigi;
    [SerializeField] float jumpPower;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1<< collision.gameObject.layer)))
        {
            if (pRigi == null)
            {
                pRigi = collision.gameObject.GetComponent<Rigidbody2D>();
            }
            pRigi.velocity = Vector2.zero;
            pRigi.AddForce(transform.up * jumpPower,ForceMode2D.Impulse);
        }
    }
}
