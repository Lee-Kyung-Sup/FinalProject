using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayMovePlatform : MonoBehaviour
{
    SpriteRenderer sp;
    Rigidbody2D rigi;
    float speed = 3;
    BoxCollider2D col;
    public static bool isStart = false;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        rigi = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        if (isStart)
        {
            transform.position += new Vector3(-speed,0,0) * Time.fixedDeltaTime;
            if (transform.position.x <= -6.47)
            {
                SetPlatform(UnityEngine.Random.Range(2,6),new Vector2(35.44f,UnityEngine.Random.Range(-5.92f, 7.17f)));
            }
        }
    }
    void SetPlatform(float colSize, Vector2 pos)
    {
        sp.size = new Vector2(colSize, sp.size.y);
        col.size = new Vector2(colSize, col.size.y);
        transform.position = pos;
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
