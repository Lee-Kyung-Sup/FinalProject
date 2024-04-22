using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;
using Unity.Loading;



public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private InventorySlot[] slots; //인벤토리 슬롯들
    private DataBaseManager theDatabase;
    private OkOrCancel theOOC;
    private Equipment theEquip;

    private List<Item> inventoryItemList; // 플레이어가 소지한 아이템리스트
    private List<Item> inventoryTabList; // 선택한 탭에 따라 전환되는 아이템리스트

    public TextMeshProUGUI Description_Text; //설명
    public string[] tabDescription; //탭설명

    public Transform tf; //slot 부모객체

    public GameObject[] selectedTabImages;
    private int selectedItem; //선택아이템
    private int selectedTab; //선택된 탭

    private bool activated; //인벤토리 활성화시 true;
    private bool itemActivated; //아이템 활성화시 true;
    private bool tabActivated; //탭 활성화시 true;
    private bool stopKeyInput; //키입력 제한(알림창시)
    private bool preventExec; //중복실행 제한

    
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    public GameObject inventoryWindow; // 인벤토리 활성화, 비활성화
    public GameObject inventoryWindow_OOC; //선택지 활성화, 비활성화

    private PlayerController controller;
    private PlayerStatus stat;

    public InventorySlot[] GetSlots() { return slots; } //저장을 위한 반환설정

    [SerializeField] private Item[] items; //item클래스에서 일치여부를 비교
    public void LoadToInven(int _itemID, string _itemName, int _itemCount) //인벤토리에서 불러올 인수
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemID == _itemID)
                slots[_itemID].AddItem(items[i], _itemCount);
        }
    }


    void Awake()
    {
        controller = GetComponent<PlayerController>();
        stat = GetComponent<PlayerStatus>();
    }
    void Start()
    {
        instance = this;
        theDatabase = FindObjectOfType<DataBaseManager>();
        theOOC = FindObjectOfType<OkOrCancel>();
        inventoryWindow.SetActive(false);
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
        theEquip = FindObjectOfType<Equipment>();
        //inventoryItemList.Add(new Item(50001, "사과", "체력을 채워주는 과일", Item.ItemType.Use));
        //inventoryItemList.Add(new Item(50003, "맥주", "체력과 기력을 채워주는 음료", Item.ItemType.Use));
    }


    public void EquipToInventory(Item _item)
    {
        inventoryItemList.Add(_item);
    }

    public void GetAnItem(int _itemID, int _count = 1)
    {
        for (int i = 0; i < theDatabase.itemList.Count; i++) //데이터베이스 아이템 검색.
        {
            if (_itemID == theDatabase.itemList[i].itemID) // 데이터베이스에 아이템 발견
            {
                for (int j = 0; j < inventoryItemList.Count; j++) //소지품에 같은 아이템이 있는지 검색.
                {
                    if (inventoryItemList[j].itemID == _itemID) //소지품에 같은 아이템이 있다 -> 개수만 증감시켜줌
                    {
                        if (inventoryItemList[j].itemType == Item.ItemType.Use)
                        {
                            inventoryItemList[j].itemCount += _count;
                            return;
                        }
                        else
                        {
                            inventoryItemList.Add(theDatabase.itemList[i]);
                        }
                        return;
                    }
                }
                inventoryItemList.Add(theDatabase.itemList[i]); //소지품에 해당 아이템 추가.
                inventoryItemList[inventoryItemList.Count - 1].itemCount = _count;
                return;
            }
        }
        Debug.LogError("데이터베이스에 해당 ID값을 가진 아이템이 존재하지 않습니다.");
    }

    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } // 인벤토리 슬롯 초기화

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } //탭 활성화
    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for (int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        Description_Text.text = tabDescription[selectedTab];
        StartCoroutine(SelectedTabEffectCoroutine());
    }  //선택된 탭을 제외하고 다른 모든 탭의 컬러 알파값 0으로 조정.
    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }

            yield return new WaitForSeconds(0.3f);
        }
    } // 선택된 탭 반짝임 효과
    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

        switch (selectedTab)
        {
            case 0:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Use == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                break;

            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Equip == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                break;

            case 2:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Quest == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                break;

            case 3:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.ETC == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                break;
        } //탭에 따른 아이템 분류 및 인벤토리 탭리스트로 추가

        for (int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].AddItem(inventoryTabList[i]);
        } //인벤토리 탭 리스트의 내용을, 인벤토리 슬롯에 추가

        SelectedItem();
    } //아이템 활성화 (탭에 맞는 아이템 분류 및 출력
    public void SelectedItem()
    {
        StopAllCoroutines();
        if (inventoryTabList.Count > 0)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryTabList.Count; i++)
            {
                slots[i].selected_Item.GetComponent<Image>().color = color;
            }
            Description_Text.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
        {
            Description_Text.text = "해당 타입의 아이템을 소유하고 있지 않습니다.";
        }
    } // 선택된 아이템 제외한 모든 탭의 컬러 알파값 0으로 조정
    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }

            yield return new WaitForSeconds(0.3f);
        }
    } //선택된 아이템 반짝임 효과.
    void Update()
    {
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated;
                if (activated)
                {
                    inventoryWindow.SetActive(true);
                    selectedTab = 0;
                    tabActivated = true;
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    StopAllCoroutines();
                    inventoryWindow.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                }




            }

            if (activated)
            {
                if (tabActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedTab < selectedTabImages.Length - 1)
                        {
                            selectedTab++;
                        }
                        else
                        {
                            selectedTab = 0;
                            SelectedTab();
                        }


                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                        {
                            selectedTab--;
                        }
                        else
                        {
                            selectedTab = selectedTabImages.Length - 1;
                            SelectedTab();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.O))
                    {
                        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true;
                        ShowItem();
                    }
                } //탭 활성화시 키입력 처리

                else if (itemActivated)
                {
                    if(inventoryTabList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 2)
                            {
                                selectedItem += 2;
                            }
                            else
                            {
                                selectedItem %= 2;
                                SelectedItem();
                            }

                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                            {
                                selectedItem -= 2;
                            }
                            else
                            {
                                selectedItem = inventoryTabList.Count - 2 - selectedItem;
                                SelectedItem();
                            }

                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 1)
                            {
                                selectedItem++;
                            }
                            else
                            {
                                selectedItem = 0;
                                SelectedItem();
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                            {
                                selectedItem--;
                            }
                            else
                            {
                                selectedItem = inventoryTabList.Count - 1;
                                SelectedItem();
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.R) && !preventExec)
                        {
                            if (selectedTab == 0) //소모품
                            {
                                StartCoroutine(OOCCoroutine("사용", "취소"));
                            }
                            else if (selectedTab == 1)
                            {
                               
                                StartCoroutine(OOCCoroutine("장착", "취소"));
                            }
                            else
                            {
                                Debug.Log("소모품선택지 및 장비장착 외의 동작");
                            }
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();
                    }

                } //아이템 활성화시 키입력 처리

                if (Input.GetKeyUp(KeyCode.R))
                {
                    preventExec = false;
                } //중복 실행 방지

            }
        }
    }

    IEnumerator OOCCoroutine(string _up, string _down)
    {
        stopKeyInput = true; //선택지 호출

        inventoryWindow_OOC.SetActive(true);
        theOOC.ShowTwoChoice(_up, _down);

        yield return new WaitUntil(() => !theOOC.activated);
        if(theOOC.GetResult())
        {
            for(int i = 0; i < inventoryTabList.Count; i++)
            {
                if (inventoryItemList[i].itemID == inventoryTabList[selectedItem].itemID)
                {
                    if(selectedTab == 0)
                    {
                        theDatabase.UseItem(inventoryItemList[i].itemID);

                        if (inventoryItemList[i].itemCount > 1)
                        {
                            inventoryItemList[i].itemCount--;
                        }
                        else
                        {
                            inventoryItemList.RemoveAt(i);
                        }

                        ShowItem();
                        break;
                    }
                    else if(selectedTab == 1)
                    {
                        theEquip.EquipItem(inventoryItemList[i]);
                        inventoryItemList.RemoveAt(i);
                        ShowItem();
                        break;
                    }


                }

            }
        }
        stopKeyInput = false;
        inventoryWindow_OOC.SetActive(false);
    }

}