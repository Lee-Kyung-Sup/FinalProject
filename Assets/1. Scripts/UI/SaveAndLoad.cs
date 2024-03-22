using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;

public class SaveAndLoad : MonoBehaviour
{
    public Inventory inv;
    public Item[] items;

    private void Start()
    {
        inv = GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    void Save()
    {
        List<ItemLoad> itemsToLoad = new List<ItemLoad>();
        for (int i = 0; i < inv.slots.Length; i++)
        {
            Slot z = inv.slots[i];
            if (z.slotsItem)
            {
                ItemLoad h = new ItemLoad(z.slotsItem.itemID, z.slotsItem.amountInStack, i);
                itemsToLoad.Add(h);
            }
        }

        string json = CustomJSON.ToJson(itemsToLoad);

        File.WriteAllText(Application.persistentDataPath + transform.name, json);

        Debug.Log("Saving.....");
    }

    void Load()
    {
        Debug.Log("Loading....");
        List<ItemLoad> itemsToLoad = CustomJSON.FromJson<ItemLoad>(File.ReadAllText(Application.persistentDataPath + transform.name));

        for (int i = itemsToLoad[0].slotIndex; i < inv.slots.Length; i++)
        {
            foreach (ItemLoad z in itemsToLoad)
            {
                if (i == z.slotIndex)
                {
                    Item b = Instantiate(items[z.id], inv.slots[i].transform).GetComponent<Item>();
                    b.amountInStack = z.amount;
                    break;
                }
            }
        }
    }

}

[System.Serializable]
public class ItemLoad
{
    public int id, amount, slotIndex;

    public ItemLoad(int id, int amount, int SLOTINDEX)
    {
        this.id = id;
        this.amount = amount;
        slotIndex = SLOTINDEX;
    }



}
