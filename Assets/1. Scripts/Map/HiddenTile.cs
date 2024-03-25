using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenTile : PlayerEnterTrigger
{
    Tilemap tile;
    protected override void Awake()
    {
        base.Awake();
        tile = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            tile.color = new Color(tile.color.r, tile.color.g, tile.color.b, 0.4f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            tile.color = new Color(tile.color.r, tile.color.g, tile.color.b, 0.4f);
        }
    }
}
