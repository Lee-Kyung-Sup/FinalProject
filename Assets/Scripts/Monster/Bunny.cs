using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bunny : Monster
{
    public Transform[] wallCheck;

    private void Awake()
    {
        base.Awake();
        moveSpeed = 2f;
        jumpPower = 15f;
    }

    void Update()
    {
        if (!isHit)
        {
            rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y); //���� �⺻ ������

            if (!Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask) &&  //��üũ 0�� �÷������ְ�
                 Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask) /*&&  //1���� �÷����̸� ���� ����
                !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask)*/)  //�÷����� �ʹ� ������ �ö󰡱� ����� ������ ����
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if (Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask))
            {
                MonsterFlip();
            }
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.transform.tag == ("PlayerHitBox"))
        {
            MonsterFlip();
        }
    }
}