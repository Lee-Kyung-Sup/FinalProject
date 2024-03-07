using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UIRoomList : UIBase<UIRoomList>
{
    [SerializeField] private UIRoom _roomInfoBase;
    [SerializeField] private Transform scrollParent;
    
    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        var roomList = RoomManager.Instance.Rooms;
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomData data = roomList[i];

            UIRoom uiRoom = Instantiate(_roomInfoBase, scrollParent);
            uiRoom.SetData(data);
            uiRoom.OpenUI();
        }
    }
}



