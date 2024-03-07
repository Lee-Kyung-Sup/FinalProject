using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour //ㅋㅍ 없으면 추가하는 기능이 있지않았나?
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
