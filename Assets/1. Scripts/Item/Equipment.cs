using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    private PlayerStatus thePlayerStat;
    private OkOrCancel theOOC;
    private Inventory theInven;

    private const int WEAPON = 0, HELMET = 1, ARMOR = 2;
    public GameObject go;
    public GameObject inventoryWindow_OOC;

    public TextMeshProUGUI[] text; //¥…∑¬ƒ°
    public Image[] img_slots; //¿Â∫ÒΩΩ∑‘æ∆¿Ãƒ‹
    public GameObject go_selected_Slot_UI; //º±≈√µ» ¿Â∫Ò ΩΩ∑‘ UI

    public Item[] equipItemPack; //¿Â¬¯µ» ¿Â∫Ò∆—

    private int selectedSlot; //º±≈√µ» ¿Â∫Ò ΩΩ∑‘

    public bool activated = false;
    private bool inputKey = true;

    // Start is called before the first frame update
    void Start()
    {
        go.SetActive(false);
        theInven = FindObjectOfType<Inventory>();
        thePlayerStat = FindObjectOfType<PlayerStatus>();
        theOOC = FindObjectOfType<OkOrCancel>();
    }

    public void EquipItem(Item _item)
    {
        string temp = _item.itemID.ToString();
        temp = temp.Substring(0, 3);
        switch(temp)
        {
            case "600": //π´±‚
                EquipItemCheck(WEAPON, _item);
                break;
            case "601": //«Ô∏‰
                EquipItemCheck(HELMET, _item);
                break;
            case "602": //∞©ø 
                EquipItemCheck(ARMOR, _item);
                break;

        }
    }




    public void EquipItemCheck(int _count, Item _item)
    {
        if (equipItemPack[_count].itemID == 0)
        {
            equipItemPack[_count] = _item;
        }
        else
        {
            theInven.EquipToInventory(equipItemPack[_count]);
            equipItemPack[_count] = _item;
        }
    }



    public void SelectedSlot()
    {
        go_selected_Slot_UI.transform.position = img_slots[selectedSlot].transform.position;
    }

    public void ClearEquip()
    {
        Color color = img_slots[0].color;
        color.a = 0f;

        for (int i = 0; i < img_slots.Length; i++)
        {
            img_slots[i].sprite = null;
            img_slots[i].color = color;
        }
    }

    public void ShowEquip()
    {
        Color color = img_slots[0].color;
        color.a = 1f;

        for (int i = 0; i < img_slots.Length; i++)
        {
            if (equipItemPack[i].itemID != 0)
            {
                img_slots[i].sprite = equipItemPack[i].itemIcon;
                img_slots[i].color = color;
            }
        }

    }






        // Update is called once per frame
      void Update()
      {
         if(inputKey)
         {
            if(Input.GetKeyDown(KeyCode.E))
            {
                activated = !activated;

                if(activated)
                {
                    go.SetActive(true);
                    selectedSlot = 0;
                    SelectedSlot();
                    ClearEquip();
                    ShowEquip();
                }
                else
                {
                    go.SetActive(false);
                    //selectedSlot = 0;
                    ClearEquip();
                }
            }

            if(activated)
            {
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if(selectedSlot < img_slots.Length - 1)
                    {
                        selectedSlot++;
                    }
                    else
                    {
                        selectedSlot = 0;
                        SelectedSlot();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (selectedSlot < img_slots.Length - 1)
                    {
                        selectedSlot++;
                    }
                    else
                    {
                        selectedSlot = 0;
                        SelectedSlot();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (selectedSlot > 0)
                    {
                        selectedSlot--;
                    }
                    else
                    {
                        selectedSlot = img_slots.Length - 1;
                        SelectedSlot();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (selectedSlot > 0)
                    {
                        selectedSlot--;
                    }
                    else
                    {
                        selectedSlot = img_slots.Length - 1;
                        SelectedSlot();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    if (equipItemPack[selectedSlot].itemID != 0)
                    {
                        inputKey = false;
                        StartCoroutine(OOCCoroutine("«ÿ¡¶", "√Îº“"));
                    }

                }
            }
            
         }
      }


    IEnumerator OOCCoroutine(string _up, string _down)
    {
        inventoryWindow_OOC.SetActive(true);
        theOOC.ShowTwoChoice(_up, _down);
        yield return new WaitUntil(() => !theOOC.activated);
        if (theOOC.GetResult())
        {
            theInven.EquipToInventory(equipItemPack[selectedSlot]);
            equipItemPack[selectedSlot] = new Item(0, "", "", Item.ItemType.Equip);
            ClearEquip();
            ShowEquip();
        }
        inputKey = true;
        inventoryWindow_OOC.SetActive(false);
    }

}
