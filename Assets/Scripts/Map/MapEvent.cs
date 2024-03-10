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

        //�÷��̾� �ൿ ���߱�
        transform.GetChild(0).gameObject.SetActive(true);//�� ���۳�Ʈ�� ù �ڽ��� Ÿ�ϸ���
        CameraController.i.CameraViewZone(transform.GetChild(1).transform.position);
        //���� ����
        //�� ������ ������ �ൿ on
        //all kill or end time

    }
    void ClearEvent()
    {
        CameraController.i.EndViewZone();
        transform.GetChild(0).gameObject.SetActive(false);
        checker.isClear[this.gameObject] = true;
    }
}
