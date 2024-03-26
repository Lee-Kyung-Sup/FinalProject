using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]



public class ItemData : ScriptableObject
{
    public string itemName; // 아이템명
    public ItemType itemType; //아이템의 유형
    public GameObject itemPrefab; // 아이템프리팹

    public enum ItemType
    {
        Equipment,
        Consume,
        Ingredient,
        ETC
    }
}



public class Item : MonoBehaviour
{

    public int amountInStack = 1; // 아이템수량
    public int maxStackSize = 1000; // 아이템총량
    public Sprite itemSprite; // 아이템이미지
    public int itemID;

}