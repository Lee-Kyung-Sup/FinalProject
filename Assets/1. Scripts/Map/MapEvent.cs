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
    MapEventChecker checker;
    BoxCollider2D col;
    Transform playerTrans;
    PlayerInput playerAction;

    [HideInInspector] public int monsters;
    [SerializeField] bool isBoss;
    Action BossMapEvent;


    BoxCollider2D stageCameraArea;
    CameraController cameraController;
    WaitUntil isAllDieMonster;
    [SerializeField] Summon[] mapPhase;
    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<BoxCollider2D>();
        isAllDieMonster = new WaitUntil(() => monsters <= 0);
        cameraController = CameraController.Instance;
    }

    private void OnEnable()
    {
        checker = MapMaker.Instance.mapEventCheker;
        if (checker.isClear.ContainsKey(transform.parent.root.name))
        {
            return;
        }
        checker.isClear.Add(transform.parent.root.name, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            col.enabled = false;
            if (!checker.isClear[transform.parent.root.name])
            {
                playerTrans = collision.transform;
                playerAction = playerTrans.GetComponent<PlayerInput>();
                StartEvent();
            }
        }
    }
    void StartEvent()
    {
        playerAction.enabled = false;//�˼� ������ ������ ������ ���� ������ �̰����� ����
        transform.GetChild(0).gameObject.SetActive(true);//�� ���۳�Ʈ�� ù �ڽ��� Ÿ�ϸ���
        stageCameraArea = cameraController.CameraArea;
        cameraController.SetCameraArea(transform.GetChild(1).GetComponent<BoxCollider2D>());//�� ��° �ڽ��� ī�޶� ���� ��ġ
        StartCoroutine(Eventing());

    }
    IEnumerator Eventing()
    {
        if (isBoss == false)
        {
            for (int i = 0; i < mapPhase.Length; i++)
            {
                for (int p = 0; p < mapPhase[i].summonMonster.Length; p++)
                {
                    CallMonster(mapPhase[i].summonMonster[p], mapPhase[i].summonPos[p]);
                }
                if (i == 0)
                {
                    //TODO �� �� ���� ���� ���� ������
                    playerAction.enabled = true;
                }
                yield return isAllDieMonster;
            }
        }
        else
        {
            BossMapEvent = MapMaker.Instance.bossMapEvents.GiveBossEvent();
            //Todo ���� ��ȯ
            //Todo ���� ��ȯ ����
            BossMapEvent?.Invoke();
        }
        ClearEvent();
    }
    void CallMonster(GameObject m, Vector3 tra)
    {
        monsters++;
        GameObject go = Instantiate(m, tra, Quaternion.identity);
        go.AddComponent<DieCount>().mapEvent = this;
    }
    void ClearEvent()
    {
        //TODO ���� ����
        cameraController.SetCameraArea(stageCameraArea);
        transform.GetChild(0).gameObject.SetActive(false);
        checker.isClear[transform.parent.root.name] = true;
    }
}
