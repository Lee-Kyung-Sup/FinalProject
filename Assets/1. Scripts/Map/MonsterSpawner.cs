using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : SingletonBase<MonsterSpawner>
{
    MonsterData monsters;
    private void Awake()
    {
        monsters = Resources.Load<MonsterData>("MonsterData");
    }
    public GameObject SpawnMonster(OrderMonster order)
    {
        return Instantiate(monsters.monsterPrefabs[(int)order.monsterIndex], order.pos, Quaternion.identity);
    }
}
