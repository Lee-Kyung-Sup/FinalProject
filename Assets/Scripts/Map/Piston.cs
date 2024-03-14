using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField]bool isUpDown;
    bool changeVector;
    Rigidbody2D rigi;
    [SerializeField]float speed;
    LayerMask gLayer;
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        gLayer = LayerMask.GetMask("Ground");
    }
    private void Update()
    {
        if (isUpDown)
        {
            if (changeVector)
            {
                rigi.velocity = Vector2.up * speed;
                return;
            }
            rigi.velocity = Vector2.down * speed;
            return;
        }
        if (changeVector)
        {
            rigi.velocity = Vector2.left * speed;
            return;
        }
        rigi.velocity = Vector2.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gLayer.value == (gLayer.value | (1 << collision.gameObject.layer)))
        {
            changeVector = !changeVector;
        }
    }
}
