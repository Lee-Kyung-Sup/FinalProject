using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public TMP_Text _name;
    public TMP_Text level;
    public TMP_Text coin;

    public GameObject[] item;

    void Start()
    {
        level.text += DataManager.instance.nowPlayer.level.ToString();
    }

    public void LevelUp()
    {
        DataManager.instance.nowPlayer.level++;
        level.text = "레벨 : " + DataManager.instance.nowPlayer.level.ToString();
    }

    public void CoinUp()
    {
        DataManager.instance.nowPlayer.coin++;
        coin.text = "코인 : " + DataManager.instance.nowPlayer.coin.ToString();
    }

    public void Save()
    {
        DataManager.instance.SaveData();
    }

    public void Load()
    {
        DataManager.instance.LoadData();
    }





    public void ItemSetting(int number)
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (number == i)
            {
                item[i].SetActive(true);
                DataManager.instance.nowPlayer.item = number;
            }
            else
            {
                item[i].SetActive(false);
            }
        }
    }

}