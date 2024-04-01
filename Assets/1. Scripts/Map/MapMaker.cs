using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMaker : SingletonBase<MapMaker>
{
    public int CurChapterId { get; private set; }
    [SerializeField] int curMapId = 0;
    GameObject curMap;
    MapDatas mapList;
    PotalMaker potalMaker;
    public MapEventChecker MapEventCheker { get; private set; }

    public BossMapEventList BossMapEvents { get; private set; }

    private void Start()
    {
        mapList = Resources.Load<MapDatas>($"Ch{CurChapterId}MapDatas");
        MapEventCheker = gameObject.AddComponent<MapEventChecker>();
        potalMaker = gameObject.AddComponent<PotalMaker>();
        BossMapEvents = new BossMapEventList();
        MakeRoom(curMapId);
    }
    public void MakeRoom(int newMap)
    {
        Destroy(curMap);
        curMapId = newMap;
        curMap = Instantiate(mapList.mapData[newMap].maps);
        CameraController.Instance.SetCameraArea(curMap.GetComponent<BoxCollider2D>());
        potalMaker.MakePotal(mapList.mapData[newMap].poter);
    }
    public void EnterChapterPotal(int chapter)
    {
        curMapId = 0;
        CurChapterId = chapter;
        mapList = Resources.Load<MapDatas>($"Ch{CurChapterId}MapDates");
        MakeRoom(curMapId);
    }
}
