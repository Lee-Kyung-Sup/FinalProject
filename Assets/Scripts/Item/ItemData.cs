using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemData
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _spritePath;
    [SerializeField] private int _value;

    public int Id => _id;
    public string Name => _name;
    public string Description => _description;
    public string SpritePath => _spritePath;
    public int Value => _value;

}
