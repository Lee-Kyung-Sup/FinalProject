using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[ExcelAsset(AssetPath = "Resources/DB", ExcelName = "ItemDbSheet")]
public class ItemDbSheet : ScriptableObject
{
	public List<ItemData> Entities; // Replace 'EntityType' to an actual type that is serializable.

}
