using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour //���� ������ �߰��ϴ� ����� �����ʾҳ�?
{
    Vector3 targetPos;
    LayerMask pLayer = LayerMask.GetMask("Player");
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value |(1<< collision.gameObject.layer)))
        {
            collision.transform.position = targetPos;
        }
    }
}
