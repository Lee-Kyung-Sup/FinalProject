using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Progress;

public class DragAndDrop : MonoBehaviour
{
    public Inventory inv;

    GameObject curSlot;
    Item curSlotsItem;

    public Image followMouseImage;

    private void Update()
    {
        followMouseImage.transform.position = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject obj = GetObjectUnderMouse();
            if (obj)
            {
                obj.GetComponent<Slot>().DropItem();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            curSlot = GetObjectUnderMouse();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (curSlot && curSlot.GetComponent<Slot>().slotsItem)
            {
                followMouseImage.color = new Color(255, 255, 255, 255);
                followMouseImage.sprite = curSlot.GetComponent<Image>().sprite;
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (curSlot && curSlot.GetComponent<Slot>().slotsItem)
            {
                curSlotsItem = curSlot.GetComponent<Slot>().slotsItem;

                GameObject newObject = GetObjectUnderMouse();
                if (newObject && newObject != curSlot)
                {
                    if (newObject.GetComponent<Slot>().slotsItem)
                    {
                        Item objectsItem = newObject.GetComponent<Slot>().slotsItem;
                        if (objectsItem.itemID == curSlotsItem.itemID && objectsItem.amountInStack != objectsItem.maxStackSize)
                        {
                            curSlotsItem.transform.parent = null;
                            inv.AddItem(curSlotsItem, objectsItem);
                        }
                        else
                        {
                            objectsItem.transform.parent = curSlot.transform;
                            curSlotsItem.transform.parent = newObject.transform;
                        }
                    }
                    else
                    {
                        curSlotsItem.transform.parent = newObject.transform;
                    }
                }
            }
        }
        else
        {
            followMouseImage.sprite = null;
            followMouseImage.color = new Color(0, 0, 0, 0);
        }


    }


    GameObject GetObjectUnderMouse()
    {
        GraphicRaycaster rayCaster = GetComponent<GraphicRaycaster>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        rayCaster.Raycast(eventData, results);

        foreach (RaycastResult i in results)
        {
            if (i.gameObject.GetComponent<Slot>())
                return i.gameObject;
        }
        return null;
    }
}
