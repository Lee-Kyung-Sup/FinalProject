using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData item;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.displayName);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Inventory.instance.AddItem(item);
            Destroy(gameObject);
        }
    }





}