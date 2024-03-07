using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[System.Serializable]
public class ItemData
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private float _value;
    [SerializeField] private ItemType _type;
    [SerializeField] private ItemGrade _grade;
    [SerializeField] private string _spritePath;
    
    public int Id => _id;
    public string Name => _name;
    public string Description => _description;
    public float Value => _value;
    public ItemType Type => _type;
    public ItemGrade Grade => _grade;
    public string SpritePath => _spritePath;

    private Sprite _sprite;

    public Sprite Sprite
    {
        get
        {
            if (_sprite == null)
                _sprite = Resources.Load<Sprite>(_spritePath);

            return _sprite;
        }
    }
} 