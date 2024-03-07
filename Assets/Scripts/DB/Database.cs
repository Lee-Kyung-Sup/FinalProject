using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Database : SingletoneBase<Database>
{
    private ItemDB _item;
    public static ItemDB Item
    {
        get
        {
            if (Instance._item == null)
                Instance._item = new ItemDB();
            return Instance._item;
        }
    }
}
