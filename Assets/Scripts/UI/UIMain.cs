using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : UIBase<UIMain>
{
    [SerializeField] private Button btnRoom;
    
    void Start()
    {
        btnRoom.onClick.AddListener(OnClickRoom);    
    }

    void OnClickRoom()
    {
        RoomManager.Instance.LoadRoomList(() =>
        {
            UIManager.Instance.ShowUI<UIRoomList>();
            CloseUI();
        });
    }
}
