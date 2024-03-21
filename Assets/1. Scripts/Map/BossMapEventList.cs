using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMapEventList
{
    public Action GiveBossEvent()
    {
        switch (MapMaker.Instance.CurChapterId)
        {
            case 0:
                return ChBoss1;
            case 1:
                return ChBoss2;
            case 2:
                return ChBoss3;
            default:
                return null;
        }
    }
    public void ChBoss1()
    {

    }
    public void ChBoss2()
    {
        OneWayMovePlatform.isStart = true;
    }
    public void ChBoss3()
    {

    }
}
