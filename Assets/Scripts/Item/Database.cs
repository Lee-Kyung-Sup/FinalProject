using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour //ΩÃ±€≈Ê
{
    private static ItemDB _item;

    public static ItemDB Item
    {
        get
        {
            if (_item == null)
                _item = new ItemDB();
            return _item;
        }
    }    
}
