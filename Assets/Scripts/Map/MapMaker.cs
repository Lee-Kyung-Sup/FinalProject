using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public static MapMaker i;
    [SerializeField]int curMapId = 0;
    GameObject curMap;
    MapDatas mapList;
    PotalMaker potalMaker;
    private void Awake()
    {
        i = this;
        mapList = Resources.Load<MapDatas>("MapDatas");
    }
    private void Start()
    {
        potalMaker = GetComponent<PotalMaker>();
        MakeRoom(curMapId);
    }
    public void MakeRoom(int newMap)
    {
        Destroy(curMap);
        curMapId = newMap;
        curMap = Instantiate(mapList.mapData[newMap].maps);
        potalMaker.MakePotal(mapList.mapData[newMap].poter);
    }
}
