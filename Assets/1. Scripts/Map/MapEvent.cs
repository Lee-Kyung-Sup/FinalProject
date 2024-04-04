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

public class MapEvent : PlayerEnterTrigger
{
    protected MapEventChecker checker;
    protected BoxCollider2D col;
    protected Transform playerTrans;
    protected PlayerInput playerAction;
    protected bool isEnter;

    [HideInInspector] public int monsters;


    protected BoxCollider2D tempCameraArea;
    protected CameraController cameraController;
    protected WaitUntil isAllDieMonster;
    [SerializeField] protected Summon[] mapPhase;
    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<BoxCollider2D>();
        isAllDieMonster = new WaitUntil(() => monsters <= 0);
        cameraController = CameraController.Instance;
    }

    protected void OnEnable()
    {
        checker = MapMaker.Instance.MapEventCheker;
        if (checker.isClear.ContainsKey(transform.parent.root.name))
        {
            return;
        }
        checker.isClear.Add(transform.parent.root.name, false);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            col.enabled = false;
            
            if (!isEnter)
            {
                isEnter = true;
                if (!checker.isClear[transform.parent.root.name])
                {
                    playerTrans = collision.transform;
                    playerAction = playerTrans.GetComponent<PlayerInput>();
                    StartEvent();
                }
            }

        }
    }
    protected void StartEvent()
    {
        playerAction.enabled = false;//알수 없지만 페이즈 설정을 하지 않으면 이곳에서 멈춤
        transform.GetChild(0).gameObject.SetActive(true);//이 컴퍼넌트의 첫 자식은 타일맵임
        tempCameraArea = cameraController.CameraArea;
        cameraController.SetCameraArea(transform.GetChild(1).GetComponent<BoxCollider2D>());//두 번째 자식은 카메라 고정 위치
        StartCoroutine(Eventing());

    }
    protected virtual IEnumerator Eventing()
    {
        for (int i = 0; i < mapPhase.Length; i++)
        {
            for (int p = 0; p < mapPhase[i].summonMonster.Length; p++)
            {
                CallMonster(mapPhase[i].summonMonster[p], mapPhase[i].summonPos[p]);
            }
            if (i == 0)
            {
                //TODO 벽 및 몬스터 생성 연출 종료후
                playerAction.enabled = true;
            }
            yield return isAllDieMonster;
        }
        ClearEvent();
    }
    protected void CallMonster(GameObject m, Vector3 tra)
    {
        monsters++;
        GameObject go = Instantiate(m, tra, Quaternion.identity);
        go.AddComponent<DieCount>().mapEvent = this;
    }
    protected void ClearEvent()
    {
        cameraController.SetCameraArea(tempCameraArea);
        transform.GetChild(0).gameObject.SetActive(false);
        checker.isClear[transform.parent.root.name] = true;
    }
}
