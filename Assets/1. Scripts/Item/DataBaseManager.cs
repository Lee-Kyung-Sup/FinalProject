using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.Rendering.DebugUI;

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
                Debug.Log("ü���� ���� ȸ���Ǿ����ϴ�");
                break;
            case 50003:
                Debug.Log("���¹̳ʰ� ȸ���Ǿ����ϴ�.");

                break;
            case 60001:
                break;
            case 60101:
                break;
            case 60201:
                break;
        }
    }

    void Start()
    {
        itemList.Add(new Item(50001, "�ݰ� ���", "�̽��� ���, ü���� ���� ȸ�� �����ݴϴ�.", Item.ItemType.Use));
        itemList.Add(new Item(50003, "���� ����", "�ÿ��� ����, ���¹̳ʸ� ���� ȸ�� �����ݴϴ�.", Item.ItemType.Use));
        itemList.Add(new Item(60001, "��ö ��", "��ī�ο� ��ö ��, ���ݷ��� [+1] �÷��ݴϴ�.", Item.ItemType.Equip));
        itemList.Add(new Item(60101, "�ż��� ��ȭ", "�߰����� ���� ���������� ��ȭ, ���ǵ带 [+3] �÷��ݴϴ�.", Item.ItemType.Equip));
        itemList.Add(new Item(60201, "�밡�� ����", "���� �������� ���� ����, ���¹̳ʸ� [+50] �ϰ�, ���� ������ ȸ����ŵ�ϴ�.", Item.ItemType.Equip));
    }

}
