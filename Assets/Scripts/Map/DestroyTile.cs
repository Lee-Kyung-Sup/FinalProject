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
        tile = GetComponent<Tilemap>();
        checker = MapMaker.Instance.mapEventCheker;
        if (checker.isBroken.ContainsKey(gameObject))
        {
            Destroy(gameObject);
            return;
        }
    }
    public void MakeDot(Vector3 pos)
    {
        Vector3Int cellPosition = tile.WorldToCell(pos);
        tile.SetTile(cellPosition, null);
        checker.isBroken.Add(gameObject,true);
    }
}
