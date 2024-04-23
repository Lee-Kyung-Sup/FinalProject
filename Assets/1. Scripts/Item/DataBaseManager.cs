using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DataBaseManager : MonoBehaviour
{
    static public DataBaseManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
    } //�̱���
    private PlayerStatus thePlayerStat;

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();

    public void UseItem(int _itemID)
    {
        switch(_itemID)
        {
            case 50001:
                //thePlayerStat.playerHealth.health++; // �̷��� �ۼ��ϸ� �����߻�
                Debug.Log("Hp�� 1����Ʈ ȸ���Ǿ����ϴ�.");
                break;
            case 50003:
                Debug.Log("Hp�� 3����Ʈ ȸ���Ǿ����ϴ�.");
                break;
        }
    }

    void Start()
    {
        itemList.Add(new Item(50001, "���", "ü���� ä���ִ� ����", Item.ItemType.Use));
        itemList.Add(new Item(50003, "����", "ü�°� ����� ä���ִ� ����", Item.ItemType.Use));
        itemList.Add(new Item(60001, "���", "ü�°� ����� ����ִ� ����", Item.ItemType.Equip));
        itemList.Add(new Item(60201, "�䰩", "ü�°� ����� �÷��ִ� ���", Item.ItemType.Equip));
        itemList.Add(new Item(60101, "����", "ü�°� ����� �÷��ִ� ���", Item.ItemType.Equip));
    }

}
