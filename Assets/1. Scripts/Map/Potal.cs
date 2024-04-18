using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : PlayerEnterTrigger
{
    [SerializeField]int goIndex;
    [SerializeField]Vector3 targetPos;
    protected override void Awake()
    {
        base.Awake();
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer))) //�Ѿ� �� ����Ʈ ����� need
        {
            CameraController.Instance.CameraOFFON();
            MapMaker.Instance.EnterPotal(goIndex);
            collision.transform.position = targetPos;
        }
    }
}
