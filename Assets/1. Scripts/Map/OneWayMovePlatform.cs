using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayMovePlatform : MonoBehaviour
{
    Rigidbody2D rigi;
    float speed;
    BoxCollider2D col;
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        //Todo if is start
        rigi.velocity = new Vector2(-speed, rigi.velocity.y);
    }
    public void SetPlatform(float speed,float colSize)
    {
        this.speed = speed;
        col.size = new Vector2(colSize, col.size.y);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IsGroundable>(out IsGroundable ound) && ound.IsGround())
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (transform.root.gameObject.activeInHierarchy && collision.gameObject.TryGetComponent<IsGroundable>(out IsGroundable ound))
        {
            collision.transform.SetParent(null);
        }
    }
}
