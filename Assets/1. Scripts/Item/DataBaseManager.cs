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


    } //싱글톤
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
                Debug.Log("체력이 전부 회복되었습니다");
                break;
            case 50003:
                Debug.Log("스태미너가 회복되었습니다.");

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
        itemList.Add(new Item(50001, "금값 사과", "싱싱한 사과, 체력을 전부 회복 시켜줍니다.", Item.ItemType.Use));
        itemList.Add(new Item(50003, "버터 맥주", "시원한 맥주, 스태미너를 전부 회복 시켜줍니다.", Item.ItemType.Use));
        itemList.Add(new Item(60001, "강철 검", "날카로운 강철 검, 공격력을 [+1] 올려줍니다.", Item.ItemType.Equip));
        itemList.Add(new Item(60101, "신속의 장화", "발걸음이 한층 가벼워지는 장화, 스피드를 [+3] 올려줍니다.", Item.ItemType.Equip));
        itemList.Add(new Item(60201, "용가죽 갑옷", "용의 가죽으로 만든 갑옷, 스태미너를 [+50] 하고, 더욱 빠르게 회복시킵니다.", Item.ItemType.Equip));
    }

}
