using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : UIBase
{
    private ItemData _data;

    [SerializeField] private Image imgGrade;
    [SerializeField] private Image imgSprite;
    [SerializeField] private TMP_Text txtName;
    
    
    public void SetSlot(int id)
    {
        var data = Database.Item.Get(id);
        SetSlot(data);
    }
    
    public void SetSlot(ItemData data)
    {
        _data = data;
        UpdataUI();
    }


    public void UpdataUI()
    {
        txtName.text = _data.Name;
        imgSprite.sprite = Instantiate(_data.Sprite);
        imgGrade.color = Colors.ItemGrade[(int)_data.Grade];
    }

    private void OnDisable()
    {
        imgSprite.sprite = null;
    }
}
