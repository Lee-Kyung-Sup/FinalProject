using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Inventory : MonoBehaviour
{
    //public GameObject inventoryObject;
    public GameObject inventoryWindow;

    public Slot[] slots;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;




    private void Start()
    {
        inventoryWindow.SetActive(false);

        foreach (Slot i in slots)
        {
            i.CustomStart();
        }
    }


    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }


    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }




    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    inventoryObject.SetActive(!inventoryObject.activeInHierarchy);
        //}

        foreach (Slot i in slots)
        {
            i.CheckForItem();
        }


    }

    public void AddItem(Item itemToBeAdded, Item startingItem = null)
    {

        int amountInStack = itemToBeAdded.amountInStack;
        List<Item> stackableItems = new List<Item>();
        List<Slot> emptyslots = new List<Slot>();

        if (startingItem && startingItem.itemID == itemToBeAdded.itemID && startingItem.amountInStack < startingItem.maxStackSize)
        {
            stackableItems.Add(startingItem);
        }

        foreach (Slot i in slots)
        {
            if (i.slotsItem)
            {
                Item z = i.slotsItem;
                if (z.itemID == itemToBeAdded.itemID && z.amountInStack < z.maxStackSize && z != startingItem)
                {
                    stackableItems.Add(z);
                }

            }
            else
            {
                emptyslots.Add(i);
            }
        }

        foreach (Item i in stackableItems)
        {
            int amountThatCanbeAdded = i.maxStackSize - i.amountInStack;
            if (amountInStack <= amountThatCanbeAdded)
            {
                i.amountInStack += amountInStack;
                Destroy(itemToBeAdded.gameObject);
                return;
            }
            else
            {
                i.amountInStack = i.maxStackSize;
                amountInStack -= amountThatCanbeAdded;
            }
        }

        itemToBeAdded.amountInStack = amountInStack;
        if (emptyslots.Count > 0)
        {
            itemToBeAdded.transform.parent = emptyslots[0].transform;
            itemToBeAdded.gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌시 아이템 습득
    {
        if (collision.GetComponent<Item>())
        {
            AddItem(collision.GetComponent<Item>());
        }
    }



}
