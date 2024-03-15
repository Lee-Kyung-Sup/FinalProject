using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : CharacterEnterTrigger
{
    [SerializeField]float upSpeed;
    [SerializeField]float downSpeed;
    [SerializeField]bool upDownCheck;
    Rigidbody2D rigi;
    List<Collision2D> Gfour = new List<Collision2D>(5);
    protected override void Awake()
    {
        base.Awake();
        rigi = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move(); //�������� �ִ� ���� ���
        //�� ������Ʈ�� piston���� ������ Gfour�� �� ��ü���� ground�� ������� ������
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
        if (characterLayer.value == (characterLayer.value | (1 << collision.gameObject.layer)))//�÷��� ó�� ó�� ���
        {
            Gfour.Add(collision);
        }
        if (LayerMask.GetMask("Ground") == (LayerMask.GetMask("Ground") | (1 << collision.gameObject.layer)))
        {
            upDownCheck = !upDownCheck;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (characterLayer.value == (characterLayer.value | (1 << collision.gameObject.layer)))
        {
            Gfour.Remove(collision);
        }
    }
}
