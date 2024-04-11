using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    MonsterData monsters;
    private void Start()
    {
        monsters = Resources.Load<MonsterData>("");
    }
}
