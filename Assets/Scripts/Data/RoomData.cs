using System;
using System.Collections.Generic;

public class RoomResult
{
    public List<RoomData> roomList;
}

[Serializable]
public class RoomData
{
    public int id;
    public string title;
    public string description;
    public int memberCount;
    public int world;
    public int avatarOption;
    public int chatOption;
    public int eventSchedule;
    public int eventOpen;
    public int eventType;
    public int game;
}