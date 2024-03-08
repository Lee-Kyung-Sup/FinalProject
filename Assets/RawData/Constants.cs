using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Consume
    }

    public enum ItemGrade
    {
        Normal,
        Magic,
        Rare,
        Unique
    }

    public class Colors
    {
        public static readonly Color[] ItemGrade = new[]
        {
            new Color(0.7f, 0.68f, 0.68f),
            new Color(0.234f, 0.56f, 0.95f),
            new Color(0.95f, 0.67f, 0.24f),
            new Color(1, 0.22f, 0.96f)
        };
    }
}
