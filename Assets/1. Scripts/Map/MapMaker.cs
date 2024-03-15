using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMaker : SingletonBase<MapMaker>
{
    [SerializeField][Range(0, 2)] int curChapterId = 0;
    [SerializeField] int curMapId = 0;
    GameObject curMap;
    MapDatas mapList;
    PotalMaker potalMaker;
    public MapEventChecker mapEventCheker { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        mapList = Resources.Load<MapDatas>($"Ch{curChapterId}MapDatas");
    }
    private void Start()
    {
        mapEventCheker = gameObject.AddComponent<MapEventChecker>();
        potalMaker = gameObject.AddComponent<PotalMaker>();
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
}
