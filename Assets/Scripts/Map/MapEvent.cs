using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEvent : MonoBehaviour
{
    MapEventChecker checker;
    BoxCollider2D col;
    LayerMask pLayer;
    Transform playerTrans;
    private void Start()
    {
        pLayer = LayerMask.GetMask("Player");
        col = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        checker = MapMaker.Instance.mapEventCheker;
        if (checker.isClear.ContainsKey(this.gameObject))
        {
            return;
        }
        checker.isClear.Add(this.gameObject,false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value |(1<< collision.gameObject.layer)))
        {
            col.enabled = false;
            if (!checker.isClear[this.gameObject])
            {
                playerTrans = collision.transform;
                StartEvent();
            }
        }
    }

    void StartEvent()
    {
        //플레이어 행동 멈추기
        transform.GetChild(0).gameObject.SetActive(true);//이 컴퍼넌트의 첫 자식은 타일맵임
        CameraController.Instance.CameraViewZone(transform.GetChild(1).transform);//두 번째 자식은 카메라 고정 위치
        //몬스터 생성
        //위 연출이 끝나면 행동 on
        //all kill or end time

    }
    void ClearEvent()
    {
        CameraController.Instance.CameraViewZone(playerTrans);
        transform.GetChild(0).gameObject.SetActive(false);
        checker.isClear[this.gameObject] = true;
    }
}
