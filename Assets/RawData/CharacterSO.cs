using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExcelAsset(AssetPath = "Resources/DB", ExcelName = "CharacterSheet")]
public class CharacterSO : ScriptableObject
{
    public List<CharacterData> Entities;
}
