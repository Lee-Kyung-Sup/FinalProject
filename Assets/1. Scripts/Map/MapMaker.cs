using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMaker : SingletonBase<MapMaker>
{
    public int curMapId;
    public bool isLoad;
    GameObject curMap;
    MapDatas mapData;
    GameObject[] mapList;
    public MapEventChecker MapEventCheker { get; private set; }

    public BossMapEventList BossMapEvents { get; private set; }

    public void StartMake(int mapId = 0)
    {
        curMapId = mapId;
        MapEventCheker = gameObject.AddComponent<MapEventChecker>();
        BossMapEvents = new BossMapEventList();
        mapData = Resources.Load<MapDatas>("MapData");
        mapList = new GameObject[mapData.mapPrefabs.Length];
        for (int i = 0; i < mapData.mapPrefabs.Length; i++)
        {
            mapList[i] = Instantiate(mapData.mapPrefabs[i]);
            mapList[i].SetActive(false);
        }
        curMap = mapList[curMapId];
        curMap.SetActive(true);
        CameraController.Instance.SetCameraArea(curMap.GetComponent<BoxCollider2D>());
    }

    public void EnterPotal(int newMap)
    {
        curMap.SetActive(false);
        curMapId = newMap;
        curMap = mapList[curMapId];
        curMap.SetActive(true);
        CameraController.Instance.SetCameraArea(curMap.GetComponent<BoxCollider2D>());
    }
    public int CurMapId()
    {
        return curMapId;
    }
}
