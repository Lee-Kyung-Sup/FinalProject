using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    LayerMask pLayer;
    Rigidbody2D pRigi;
    [SerializeField] float jumpPower;
    private void Start()
    {
        pLayer = LayerMask.GetMask("Player");
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
            pRigi.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
        }
    }
}
