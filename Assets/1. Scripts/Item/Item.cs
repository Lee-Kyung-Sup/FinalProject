using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]



public class ItemData : ScriptableObject
{
    public string itemName; // �����۸�
    public ItemType itemType; //�������� ����
    public GameObject itemPrefab; // ������������

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

    public int amountInStack = 1; // �����ۼ���
    public int maxStackSize = 1000; // �������ѷ�
    public Sprite itemSprite; // �������̹���
    public int itemID;

}