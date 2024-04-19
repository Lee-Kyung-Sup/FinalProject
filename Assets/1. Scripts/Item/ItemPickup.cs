using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID;
    public int _count;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Inventory.instance.GetAnItem(itemID, _count);
            Destroy(this.gameObject); //인벤토리 추가
        }
    }
}
