using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaterPotal : PlayerEnterTrigger
{
    [SerializeField] int gochapter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            MapMaker.Instance.EnterChapterPotal(gochapter);
        }
    }
}
