using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName_Text;
    public TextMeshProUGUI itemCount_Text;
    public GameObject selected_Item;

    public int itemCount;//추가수정

    // Start is called before the first frame update
    public void AddItem(Item _item)
    {
        itemName_Text.text = _item.itemName;
        //itemCount_Text.text = itemCount.ToString();//추가수정
        icon.sprite = _item.itemIcon;

        if (Item.ItemType.Use == _item.itemType)
        {
            if (_item.itemCount > 0)
            {
                itemCount_Text.text = "x" + _item.itemCount.ToString();
            }
            else
            {
                itemCount_Text.text = "";
            }
        }
    }

    public void RemoveItem()
    {
        itemName_Text.text = "";
        itemCount_Text.text = "";
        icon.sprite = null;
    }
}
