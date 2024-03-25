using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item slotsItem;

    Sprite defaultSprite;
    TMP_Text amountText;

    public void CustomStart()
    {
        defaultSprite = GetComponent<Image>().sprite;
        amountText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void DropItem()
    {
        if (slotsItem)
        {
            slotsItem.transform.parent = null;
            slotsItem.gameObject.SetActive(true);
            slotsItem.transform.position = Vector3.zero;
        }
    }

    public void CheckForItem()
    {
        if (transform.childCount > 1)
        {
            //if (slotsItem == null)
            //       return;

           slotsItem = transform.GetChild(1).GetComponent<Item>();
            GetComponent<Image>().sprite = slotsItem.itemSprite;
            if (slotsItem.amountInStack > 1)
            {
                amountText.text = slotsItem.amountInStack.ToString();
            }
        }
        else
        {
            slotsItem = null;
            GetComponent<Image>().sprite = defaultSprite;
            amountText.text = "";
        }
    }


}
