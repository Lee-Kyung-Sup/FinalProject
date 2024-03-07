using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemDB
{
    private Dictionary<int, ItemData> _items = new ();
    
    public ItemDB()
    {
        var res = Resources.Load<ItemSO>("DB/ItemSO");
        var itemSo = Object.Instantiate(res);
        var entities = itemSo.Entities;
        
        if(entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var item = entities[i];
            
            if (_items.ContainsKey(item.Id))
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
