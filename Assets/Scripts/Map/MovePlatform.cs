using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : CharacterEnterTrigger
{
    [SerializeField] bool isUpDown;
    [SerializeField] float speed;
    [SerializeField] float amplitude;
    Vector2 startPos;
    Rigidbody2D rigi;
    protected override void Awake()
    {
        base.Awake();
        rigi = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }
    private void Update()
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (characterLayer.value ==(characterLayer.value | (1<<collision.gameObject.layer)))
        {
            if (collision.gameObject.GetComponent<IsGroundable>().IsGround())
            {
                collision.transform.SetParent(transform);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (transform.root.gameObject.activeInHierarchy && characterLayer.value == (characterLayer.value | (1 << collision.gameObject.layer)))
        {
            collision.transform.SetParent(null);
        }
    }
}
