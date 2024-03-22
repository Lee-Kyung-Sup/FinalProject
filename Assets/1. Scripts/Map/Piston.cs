using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField]float upSpeed;
    [SerializeField]float downSpeed;
    [SerializeField]bool upDownCheck;
    Rigidbody2D rigi;
    LayerMask fLayer;
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        fLayer = LayerMask.GetMask("Ground","Platform");
    }
    private void FixedUpdate()
    {
        Move();
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
        if (fLayer.value == (fLayer.value | (1 << collision.gameObject.layer)))
        {
            upDownCheck = !upDownCheck;
        }
    }
}
