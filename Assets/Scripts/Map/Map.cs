using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    PotalDataList potals;
    private void Start()
    {
        potals = Resources.Load<PotalDataList>("temp");
        for (int i = 0; i < potals.potalDatas.Count; i++)
        {
            GameObject go = new GameObject();
            go.transform.position = potals.potalDatas[i].pos;
            go.AddComponent<BoxCollider2D>().isTrigger = true;
            go.AddComponent<Potal>();
        }
    }
}
