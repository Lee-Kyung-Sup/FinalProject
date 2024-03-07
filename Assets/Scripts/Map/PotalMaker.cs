using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalMaker : MonoBehaviour
{
    PotalDataList potals;
    private void Start()
    {
        potals = Resources.Load<PotalDataList>("Potals");
        for (int i = 0; i < potals.potalDatas.Count; i++)
        {
            GameObject go = new GameObject();
            PotalData temp = potals.potalDatas[i];
            go.transform.position = temp.pos;
            go.AddComponent<BoxCollider2D>().isTrigger = true;
            go.GetComponent<BoxCollider2D>().size = temp.size;
            go.AddComponent<Potal>();
        }
    }
}
