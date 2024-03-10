using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEvent : MonoBehaviour
{
    MapEventChecker checker;
    BoxCollider2D col;
    LayerMask pLayer;
    private void Start()
    {
        pLayer = LayerMask.GetMask("Player");
        col = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        checker = MapEventChecker.i;
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
                StartEvent();
            }
        }
    }

    void StartEvent()
    {

        //플레이어 행동 멈추기
        transform.GetChild(0).gameObject.SetActive(true);//이 컴퍼넌트의 첫 자식은 타일맵임
        CameraController.i.CameraViewZone(transform.GetChild(1).transform.position);
        //몬스터 생성
        //위 연출이 끝나면 행동 on
        //all kill or end time

    }
    void ClearEvent()
    {
        CameraController.i.EndViewZone();
        transform.GetChild(0).gameObject.SetActive(false);
        checker.isClear[this.gameObject] = true;
    }
}
