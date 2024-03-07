using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoom : UIBase<UIRoom>
{
    private RoomData _roomData;
    
    [SerializeField] private TMP_Text _txtTitle;
    [SerializeField] private TMP_Text _txtDesc;
    [SerializeField] private TMP_Text _txtCount;

    public void SetData(RoomData data)
    {
        _roomData = data;
        
        _txtTitle.text = data.title;
        _txtDesc.text = data.description;
        _txtCount.text = $"{data.memberCount}/5";
    }
}
