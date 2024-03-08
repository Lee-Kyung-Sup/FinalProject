using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] bool isUpDown;
    [SerializeField] float speed;
    [SerializeField] float amplitude;
    Vector2 startPos;
    Rigidbody2D rigi;
    LayerMask pLayer;
    LayerMask mLayer;
    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        pLayer = LayerMask.GetMask("Player");
        mLayer = LayerMask.GetMask("Monster");
    }
    private void FixedUpdate()
    {
        if (isUpDown)
        {
            rigi.transform.position = new Vector2(startPos.x, startPos.y + amplitude * Mathf.Sin(Time.time) * speed);
        }
        else
        {
            rigi.transform.position = new Vector2(startPos.x + amplitude * Mathf.Sin(Time.time) * speed, startPos.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) //Fix later
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)) && collision.gameObject.GetComponent<PlayerMovement>().IsGrounded)
        {
            collision.transform.SetParent(transform);
        }
        else if(mLayer.value == (mLayer.value | (1 << collision.gameObject.layer)) && collision.gameObject.GetComponent<Monster>().isGround)
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            collision.transform.SetParent(null);
        }
        else if (mLayer.value == (mLayer.value | (1 << collision.gameObject.layer)))
        {
            collision.transform.SetParent(null);
        }
    }
}
