using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public static MapMaker i;
    [SerializeField]int curMapId = 0;
    GameObject curMap;
    MapData mapList;
    PotalMaker potalMaker;
    private void Awake()
    {
        i = this;
        mapList = Resources.Load<MapData>("MapDatas");
    }
    private void Start()
    {
        potalMaker = GetComponent<PotalMaker>();
        MakeRoom(curMapId);
    }
    public void MakeRoom(int newMap)
    {
        Destroy(curMap);
        curMap = Instantiate(mapList.mapData[newMap].maps);
        potalMaker.MakePotal(mapList.mapData[newMap].poter);
    }
}
