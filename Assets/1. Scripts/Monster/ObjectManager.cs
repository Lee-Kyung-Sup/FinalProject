using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject PlayerBulletPrefab;
    public GameObject PlayerMeleeHitPrefab;
    public GameObject PlayerRangeHitPrefab;

    public GameObject bulletBossAPrefab;
    public GameObject enemyBPrefab;

    GameObject[] PlayerBullet;
    GameObject[] PlayerMeleeHit;
    GameObject[] PlayerRangeHit;
    GameObject[] enemyB;
    GameObject[] bulletBossA;
    GameObject[] targetPool;
    private List<GameObject> frogbullet = new List<GameObject>();

    void Awake()
    {
        PlayerBullet = new GameObject[10];
        PlayerMeleeHit = new GameObject[5];
        PlayerRangeHit = new GameObject[5];

        enemyB = new GameObject[1];
        bulletBossA = new GameObject[300];

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < PlayerBullet.Length; i++)
        {
            PlayerBullet[i] = Instantiate(PlayerBulletPrefab);
            PlayerBullet[i].SetActive(false);
        }

        for (int i = 0; i < PlayerMeleeHit.Length; i++)
        {
            PlayerMeleeHit[i] = Instantiate(PlayerMeleeHitPrefab);
            PlayerMeleeHit[i].SetActive(false);
        }

        for (int i = 0; i < PlayerRangeHit.Length; i++)
        {
            PlayerRangeHit[i] = Instantiate(PlayerRangeHitPrefab);
            PlayerRangeHit[i].SetActive(false);
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
            case "PlayerBullet":
                targetPool = PlayerBullet;
                break;
            case "PlayerMeleeHit":
                targetPool = PlayerMeleeHit;
                break;
            case "PlayerRangeHit":
                targetPool = PlayerRangeHit;
                break;
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
            case "PlayerBullet":
                targetPool = PlayerBullet;
                break;
            case "PlayerMeleeHit":
                targetPool = PlayerMeleeHit;
                break;
            case "PlayerRangeHit":
                targetPool = PlayerRangeHit;
                break;
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
