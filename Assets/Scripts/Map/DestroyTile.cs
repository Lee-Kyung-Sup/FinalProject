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
        if (checker.isBroken.ContainsKey(transform.parent.name))
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            MakeDot(Vector3.zero);
        }
    }
    public void MakeDot(Vector3 pos)
    {
        Vector3Int cellPosition = tile.WorldToCell(pos);
        tile.SetTile(cellPosition, null);
        checker.isBroken.Add(transform.parent.name, true);
    }
}
