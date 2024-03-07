using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTile : MonoBehaviour
{
    Tilemap tile;
    private void Start()
    {
        tile = GetComponent<Tilemap>();
    }
    public void MakeDot(Vector3 pos)
    {
        Vector3Int cellPosition = tile.WorldToCell(pos);
        tile.SetTile(cellPosition, null);
    }
}
