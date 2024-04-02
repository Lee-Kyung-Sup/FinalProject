using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaterPotal : PlayerEnterTrigger
{
    [SerializeField] int gochapter;
    [SerializeField] int goMapId;
    [SerializeField] Vector2 targetPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            collision.transform.position = targetPos;
            MapMaker.Instance.EnterChapterPotal(goMapId, gochapter);
        }
    }
}