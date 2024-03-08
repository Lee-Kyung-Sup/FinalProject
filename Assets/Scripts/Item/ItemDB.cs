using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    private Dictionary<int, ItemData> _items = new();

    public ItemDB()
    {
        var res = Resources.Load<ItemDbSheet>(path: "DB/ItemDbSheet");
        var itemSo = Object.Instantiate(res);
        var entities = itemSo.Entities;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; ++i)
        {
            var item = entities[i];
            if(_items.ContainsKey(item.Id))
                _items[item.Id] = item;
            else
                _items.Add(item.Id, item);
        }
    }

    public ItemData Get(int id)
    {
        if (_items.ContainsKey(id))
            return _items[id];
        return null;
    }

    public IEnumerator DbEnumerator()
    {
        return _items.GetEnumerator();
    }

}
