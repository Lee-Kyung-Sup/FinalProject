using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject bulletBossAPrefab;
    public GameObject enemyBPrefab;

    GameObject[] enemyB;
    GameObject[] bulletBossA;
    GameObject[] targetPool;

    void Awake()
    {
        enemyB = new GameObject[1];
        bulletBossA = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);
        }

        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "enemyB":
                targetPool = enemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
        }   
        for(int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "enemyB":
                targetPool = enemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
        }

        return targetPool;
    }
}
