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
                //thePlayerStat.playerHealth.health++; // 이렇게 작성하면 오류발생
                Debug.Log("Hp가 1포인트 회복되었습니다.");
                break;
            case 50003:
                Debug.Log("Hp가 3포인트 회복되었습니다.");
                break;
        }
    }

    void Start()
    {
        itemList.Add(new Item(50001, "사과", "체력을 채워주는 과일", Item.ItemType.Use));
        itemList.Add(new Item(50003, "맥주", "체력과 기력을 채워주는 음료", Item.ItemType.Use));
        itemList.Add(new Item(60001, "장검", "체력과 기력을 깍아주는 무기", Item.ItemType.Equip));
        itemList.Add(new Item(60201, "흉갑", "체력과 기력을 올려주는 장비", Item.ItemType.Equip));
        itemList.Add(new Item(60101, "원주", "체력과 기력을 올려주는 장비", Item.ItemType.Equip));
    }

}
