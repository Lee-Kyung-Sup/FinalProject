using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;

public class RoomManager : SingletoneBase<RoomManager>
{
    public List<RoomData> Rooms = new ();
    
    public void LoadRoomList(Action callback)
    {
        HttpRequest.Send(
            url: ServerUrl.roomList, 
            httpMethod: HTTPMethod.GET,
            completeCallback: (response) =>
            {
                if (response.IsSuccess == false)
                    return;
                
                var result = JsonUtility.FromJson<RoomResult>(response.ResultString);
                Rooms = result.roomList;

                callback?.Invoke();
            }
        );
    }
}