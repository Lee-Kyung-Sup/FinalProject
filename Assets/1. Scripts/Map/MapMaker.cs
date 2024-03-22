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
    public MapEventChecker mapEventCheker { get; private set; }

    public BossMapEventList bossMapEvents { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        mapList = Resources.Load<MapDatas>($"Ch{CurChapterId}MapDatas");
        mapEventCheker = gameObject.AddComponent<MapEventChecker>();
        potalMaker = gameObject.AddComponent<PotalMaker>();
        bossMapEvents = new BossMapEventList();
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
    public void ClearChapter()
    {
        CurChapterId++;
        curMapId = 0;
        Destroy(curMap);
        mapList = Resources.Load<MapDatas>($"Ch{CurChapterId}MapDatas");
        MakeRoom(curMapId);
    }
}
