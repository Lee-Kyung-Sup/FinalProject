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
        tile.SetTile(cellPosition + Vector3Int.left, null);
        tile.SetTile(cellPosition + Vector3Int.right, null);
        tile.SetTile(cellPosition + Vector3Int.up, null);
        tile.SetTile(cellPosition + Vector3Int.down, null);
        tile.SetTile(cellPosition + new Vector3Int(1, 1, 0), null);
        tile.SetTile(cellPosition + new Vector3Int(-1, -1, 0), null);
        tile.SetTile(cellPosition + new Vector3Int(1, -1, 0), null);
        tile.SetTile(cellPosition + new Vector3Int(-1, 1, 0), null);
        tile.SetTile(cellPosition, null);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMeleeAttackHandler>(out PlayerMeleeAttackHandler p))
        {
            MakeDot(collision.transform.position);
        }
    }
}
