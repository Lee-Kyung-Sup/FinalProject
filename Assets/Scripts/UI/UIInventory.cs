using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : UIBase
{
    [SerializeField] private Transform _contentParent;

    public override void OpenUI()
    {
        base.OpenUI();
        SetItems();
    }

    void SetItems()
    {
        var inventory = UserManager.Instance.InventoryItems;
        var itemCount = inventory.Count;
        
        for (int i = 0; i < itemCount; ++i)
        {
            var slot = UIManager.Instance.CreateUI<UIItemSlot>(_contentParent);
            slot.SetSlot(inventory[i]);
        }
    }
}
