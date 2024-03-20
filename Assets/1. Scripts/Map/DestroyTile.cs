using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTile : MonoBehaviour
{
    Tilemap tile;
    MapEventChecker checker;
    private void Start()
    {
        Debug.Log(transform.parent.root.name);
        tile = GetComponent<Tilemap>();
        checker = MapMaker.Instance.mapEventCheker;
        if (checker.isBroken.ContainsKey(transform.parent.root.name))
        {
            if (checker.isBroken[transform.parent.root.name])
            {
                Destroy(gameObject);
                return;
            }
            return;
        }
        checker.isBroken.Add(transform.parent.root.name, false);
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
        checker.isBroken[transform.parent.root.name] = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMeleeAttackHandler>(out PlayerMeleeAttackHandler p))
        {
            MakeDot(collision.transform.position);
        }
    }
}
