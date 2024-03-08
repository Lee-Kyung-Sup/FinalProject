using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ItemDbSheet : ScriptableObject
{
	public List<ItemData> Entities; // Replace 'EntityType' to an actual type that is serializable.

}
