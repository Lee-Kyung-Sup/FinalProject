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
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (isUpDown)
        {
            rigi.transform.position = new Vector2(startPos.x, startPos.y + amplitude * Mathf.Sin(Time.time * speed));
        }
        else
        {
            rigi.transform.position = new Vector2(startPos.x + amplitude * Mathf.Sin(Time.time * speed), startPos.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
