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
            case 1:
                return ChBoss1;
            case 2:
                return ChBoss2;
            case 3:
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

    }
    public void ChBoss3()
    {

    }
}
