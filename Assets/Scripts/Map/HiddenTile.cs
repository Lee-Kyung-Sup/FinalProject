using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenTile : MonoBehaviour
{
    Tilemap tile;
    LayerMask pLayer;
    private void Start()
    {
        tile = GetComponent<Tilemap>();
        pLayer = LayerMask.GetMask("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            Debug.Log("a");
            tile.color = new Color(tile.color.r, tile.color.g, tile.color.b, 0.4f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            tile.color = new Color(tile.color.r, tile.color.g, tile.color.b, 1);
        }
    }
}
