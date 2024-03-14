using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotalMaker : MonoBehaviour
{
    List<GameObject> potals = new List<GameObject>(5);
    public void MakePotal(Poter[] poters)
    {
        DestroyPotals();
        potals.Clear();
        for (int i = 0; i < poters.Length; i++)
        {
            GameObject go = new GameObject();
            go.AddComponent<BoxCollider2D>().size = poters[i].potalSize;
            go.transform.position = poters[i].potalPos;
            go.AddComponent<Potal>().goIndex = poters[i].goMapIndex;
            go.GetComponent<Potal>().targetPos = poters[i].targetPos;
            potals.Add(go);
        }
    }
    void DestroyPotals()
    {
        foreach (var item in potals)
        {
            Destroy(item);
        }
        potals.Clear();
    }
}
