using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItem : PlayerEnterTrigger
{
    [SerializeField] Paction actionType;
    MapEventChecker checker;
    private void Start()
    {
        checker = MapMaker.Instance.MapEventCheker;
        if (!checker.isGet.ContainsKey(actionType))
        {
            checker.isGet.Add(actionType, false);
            return;
        }
        if (checker.isGet[actionType])
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            //Todo GetUi
            //collision.GetComponent<PlayerController>().UnLockAction(actionType);
            checker.isGet[actionType] = true;
            Destroy(gameObject);
        }
    }
}
