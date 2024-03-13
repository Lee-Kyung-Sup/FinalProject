using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[Serializable]
public class Summon
{
    public GameObject[] summonMonster;
    public Vector3[] summonPos;
}

public class MapEvent : MonoBehaviour
{
    MapEventChecker checker;
    BoxCollider2D col;
    LayerMask pLayer;
    Transform playerTrans;
    PlayerInput playerAction;

    [HideInInspector] public int monsters;

    WaitUntil isAllDieMonster;
    [SerializeField] Summon[] mapPhase;

    private void Start()
    {
        pLayer = LayerMask.GetMask("Player");
        col = GetComponent<BoxCollider2D>();
        isAllDieMonster = new WaitUntil(() => monsters <= 0);
    }

    private void OnEnable()
    {
        checker = MapMaker.Instance.mapEventCheker;
        if (checker.isClear.ContainsKey(transform.parent.name))
        {
            return;
        }
        checker.isClear.Add(transform.parent.name, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value |(1<< collision.gameObject.layer)))
        {
            col.enabled = false;
            if (!checker.isClear[transform.parent.name])
            {
                playerTrans = collision.transform;
                playerAction = playerTrans.GetComponent<PlayerInput>();
                StartEvent();
            }
        }
    }
    void StartEvent()
    {
        playerAction.enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);//이 컴퍼넌트의 첫 자식은 타일맵임
        CameraController.Instance.CameraViewZone(transform.GetChild(1).transform);//두 번째 자식은 카메라 고정 위치
        StartCoroutine(Eventing());
    }
    IEnumerator Eventing()
    {
        for (int i = 0; i < mapPhase.Length; i++)
        {
            for (int p = 0; p < mapPhase[i].summonMonster.Length; p++)
            {
                CallMonster(mapPhase[i].summonMonster[p], mapPhase[i].summonPos[p]);
            }
            //연출이 끝날때까지 기달
            playerAction.enabled = true;
            yield return isAllDieMonster;
        }
        ClearEvent();
    }
    void CallMonster(GameObject m,Vector3 tra)
    {
        monsters++;
        GameObject go = Instantiate(m, tra, Quaternion.identity);
        go.AddComponent<DieCount>().mapEvent = this;
    }
    void ClearEvent()
    {
        CameraController.Instance.CameraViewZone(playerTrans);
        transform.GetChild(0).gameObject.SetActive(false);
        checker.isClear[transform.parent.name] = true;
    }
}
