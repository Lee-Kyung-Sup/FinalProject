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
    private InventorySlot[] slots; //�κ��丮 ���Ե�
    private DataBaseManager theDatabase;
    private OkOrCancel theOOC;
    private Equipment theEquip;

    private List<Item> inventoryItemList; // �÷��̾ ������ �����۸���Ʈ
    private List<Item> inventoryTabList; // ������ �ǿ� ���� ��ȯ�Ǵ� �����۸���Ʈ

    public TextMeshProUGUI Description_Text; //����
    public string[] tabDescription; //�Ǽ���

    public Transform tf; //slot �θ�ü

    public GameObject[] selectedTabImages;
    private int selectedItem; //���þ�����
    private int selectedTab; //���õ� ��

    private bool activated; //�κ��丮 Ȱ��ȭ�� true;
    private bool itemActivated; //������ Ȱ��ȭ�� true;
    private bool tabActivated; //�� Ȱ��ȭ�� true;
    private bool stopKeyInput; //Ű�Է� ����(�˸�â��)
    private bool preventExec; //�ߺ����� ����

    
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    public GameObject inventoryWindow; // �κ��丮 Ȱ��ȭ, ��Ȱ��ȭ
    public GameObject inventoryWindow_OOC; //������ Ȱ��ȭ, ��Ȱ��ȭ

    private PlayerController controller;
    private PlayerStatus stat;

    public InventorySlot[] GetSlots() { return slots; } //������ ���� ��ȯ����

    [SerializeField] private Item[] items; //itemŬ�������� ��ġ���θ� ��
    public void LoadToInven(int _itemID, string _itemName, int _itemCount) //�κ��丮���� �ҷ��� �μ�
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
        //inventoryItemList.Add(new Item(50001, "���", "ü���� ä���ִ� ����", Item.ItemType.Use));
        //inventoryItemList.Add(new Item(50003, "����", "ü�°� ����� ä���ִ� ����", Item.ItemType.Use));
    }


    public void EquipToInventory(Item _item)
    {
        inventoryItemList.Add(_item);
    }

    public void GetAnItem(int _itemID, int _count = 1)
    {
        for (int i = 0; i < theDatabase.itemList.Count; i++) //�����ͺ��̽� ������ �˻�.
        {
            if (_itemID == theDatabase.itemList[i].itemID) // �����ͺ��̽��� ������ �߰�
            {
                for (int j = 0; j < inventoryItemList.Count; j++) //����ǰ�� ���� �������� �ִ��� �˻�.
                {
                    if (inventoryItemList[j].itemID == _itemID) //����ǰ�� ���� �������� �ִ� -> ������ ����������
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
                inventoryItemList.Add(theDatabase.itemList[i]); //����ǰ�� �ش� ������ �߰�.
                inventoryItemList[inventoryItemList.Count - 1].itemCount = _count;
                return;
            }
        }
        Debug.LogError("�����ͺ��̽��� �ش� ID���� ���� �������� �������� �ʽ��ϴ�.");
    }

    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } // �κ��丮 ���� �ʱ�ȭ

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } //�� Ȱ��ȭ
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
    }  //���õ� ���� �����ϰ� �ٸ� ��� ���� �÷� ���İ� 0���� ����.
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
    } // ���õ� �� ��¦�� ȿ��
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
        } //�ǿ� ���� ������ �з� �� �κ��丮 �Ǹ���Ʈ�� �߰�

        for (int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].AddItem(inventoryTabList[i]);
        } //�κ��丮 �� ����Ʈ�� ������, �κ��丮 ���Կ� �߰�

        SelectedItem();
    } //������ Ȱ��ȭ (�ǿ� �´� ������ �з� �� ���
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
            Description_Text.text = "�ش� Ÿ���� �������� �����ϰ� ���� �ʽ��ϴ�.";
        }
    } // ���õ� ������ ������ ��� ���� �÷� ���İ� 0���� ����
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
    } //���õ� ������ ��¦�� ȿ��.
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
                } //�� Ȱ��ȭ�� Ű�Է� ó��

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
                            if (selectedTab == 0) //�Ҹ�ǰ
                            {
                                StartCoroutine(OOCCoroutine("���", "���"));
                            }
                            else if (selectedTab == 1)
                            {
                               
                                StartCoroutine(OOCCoroutine("����", "���"));
                            }
                            else
                            {
                                Debug.Log("�Ҹ�ǰ������ �� ������� ���� ����");
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

                } //������ Ȱ��ȭ�� Ű�Է� ó��

                if (Input.GetKeyUp(KeyCode.R))
                {
                    preventExec = false;
                } //�ߺ� ���� ����

            }
        }
    }

    IEnumerator OOCCoroutine(string _up, string _down)
    {
        stopKeyInput = true; //������ ȣ��

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