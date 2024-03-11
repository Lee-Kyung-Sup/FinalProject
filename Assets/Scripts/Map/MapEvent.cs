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
        //�÷��̾� �ൿ ���߱�
        transform.GetChild(0).gameObject.SetActive(true);//�� ���۳�Ʈ�� ù �ڽ��� Ÿ�ϸ���
        CameraController.Instance.CameraViewZone(transform.GetChild(1).transform);//�� ��° �ڽ��� ī�޶� ���� ��ġ
        //���� ����
        //�� ������ ������ �ൿ on
        //all kill or end time

    }
    void ClearEvent()
    {
        CameraController.Instance.CameraViewZone(playerTrans);
        transform.GetChild(0).gameObject.SetActive(false);
        checker.isClear[this.gameObject] = true;
    }
}
