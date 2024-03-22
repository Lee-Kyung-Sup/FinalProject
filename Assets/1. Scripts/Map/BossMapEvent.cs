using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMapEvent : MapEvent
{
    Action BossMapEventF;
    protected override IEnumerator Eventing()
    {
        for (int i = 0; i < mapPhase.Length; i++)
        {
            for (int p = 0; p < mapPhase[i].summonMonster.Length; p++)
            {
                CallMonster(mapPhase[i].summonMonster[p], mapPhase[i].summonPos[p]);
            }
            if (i == 0)
            {
                //TODO �� �� ���� ���� ���� ������
                playerAction.enabled = true;
                BossMapEventF = MapMaker.Instance.BossMapEvents.GiveBossEvent();
                BossMapEventF?.Invoke();
            }
            yield return isAllDieMonster;
        }
        ClearEvent();
    }
}
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
    void ChBoss1()
    {

    }
    void ChBoss2()
    {
        OneWayMovePlatform.isStart = true;
    }
    void ChBoss3()
    {

    }
}