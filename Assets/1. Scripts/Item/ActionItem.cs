using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItem : PlayerEnterTrigger
{
    [SerializeField] Paction actionType;
    private SkillPanelController skillPanelController;
    MapEventChecker checker;
    private void Start()
    {
        skillPanelController = FindObjectOfType<SkillPanelController>();

        checker = MapMaker.Instance.MapEventCheker;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pLayer.value == (pLayer.value | (1 << collision.gameObject.layer)))
        {
            //Todo GetUi
            collision.GetComponent<PlayerController>().UnLockAction(actionType);


            if (skillPanelController != null)
            {
                skillPanelController.ShowSkillPopup(actionType);
            }

            Destroy(gameObject);
        }
    }
}
