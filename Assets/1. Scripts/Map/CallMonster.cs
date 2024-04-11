using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMonster : MonoBehaviour
{
    [SerializeField] OrderMonster[] callList;
    GameObject[] mapMonster;
    private void OnEnable()
    {
        if (mapMonster == null)
        {
            mapMonster = new GameObject[callList.Length];
        }
        for (int i = 0; i < callList.Length; i++)
        {
            mapMonster[i] = MonsterSpawner.Instance.SpawnMonster(callList[i]);
        }
    }
    private void OnDisable()
    {
        foreach (var item in mapMonster)
        {
            Destroy(item);
        }
    }
}
[Serializable]
public class OrderMonster
{
    public int monsterIndex;
    public Vector2 pos;
}
