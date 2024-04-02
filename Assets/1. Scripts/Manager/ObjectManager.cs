using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public GameObject PlayerBulletPrefab;
    public GameObject PlayerChargeBulletPrefab;
    public GameObject PlayerMeleeHitPrefab;
    public GameObject PlayerRangeHitPrefab;
    public GameObject PlayerJumpHitEffectPrefab;
    public GameObject PlayerDeflectEffectPrefab;

    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBTPrefab;
    public GameObject enemyBPrefab;
    public GameObject enemyBTPrefab;

    GameObject[] PlayerBullet;
    GameObject[] PlayerChargeBullet;
    GameObject[] PlayerMeleeHit;
    GameObject[] PlayerRangeHit;
    GameObject[] PlayerJumpHit;
    GameObject[] PlayerDeflectEffect;
    GameObject[] enemyB;
    GameObject[] enemyBT;
    GameObject[] bulletBossA;
    GameObject[] bulletBossBT;
    GameObject[] targetPool;
    private List<GameObject> frogbullet = new List<GameObject>();

    public static ObjectManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerBullet = new GameObject[45];
        PlayerChargeBullet = new GameObject[5];
        PlayerMeleeHit = new GameObject[10];
        PlayerRangeHit = new GameObject[15];
        PlayerJumpHit = new GameObject[15];
        PlayerDeflectEffect = new GameObject[10];

        enemyB = new GameObject[1];
        enemyBT = new GameObject[1];
        bulletBossA = new GameObject[300];
        bulletBossBT = new GameObject[300];

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < PlayerBullet.Length; i++)
        {
            PlayerBullet[i] = Instantiate(PlayerBulletPrefab);
            PlayerBullet[i].SetActive(false);
        }

        for (int i = 0; i < PlayerChargeBullet.Length; i++)
        {
            PlayerChargeBullet[i] = Instantiate(PlayerChargeBulletPrefab);
            PlayerChargeBullet[i].SetActive(false);
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

        for (int i = 0; i < PlayerJumpHit.Length; i++)
        {
            PlayerJumpHit[i] = Instantiate(PlayerJumpHitEffectPrefab);
            PlayerJumpHit[i].SetActive(false);
        }

        for (int i = 0; i < PlayerDeflectEffect.Length; i++)
        {
            PlayerDeflectEffect[i] = Instantiate(PlayerDeflectEffectPrefab);
            PlayerDeflectEffect[i].SetActive(false);
        }

        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossBT.Length; index++)
        {
            bulletBossBT[index] = Instantiate(bulletBossBTPrefab);
            bulletBossBT[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "PlayerBullet":
                targetPool = PlayerBullet;
                break;
            case "PlayerChargeBullet":
                targetPool = PlayerChargeBullet;
                break;
            case "PlayerMeleeHit":
                targetPool = PlayerMeleeHit;
                break;
            case "PlayerRangeHit":
                targetPool = PlayerRangeHit;
                break;
            case "PlayerJumpHit":
                targetPool = PlayerJumpHit;
                break;
            case "PlayerDeflectEffect":
                targetPool = PlayerDeflectEffect;
                break;
            case "enemyB":
                targetPool = enemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "enemyBT":
                targetPool = enemyBT;
                break;
            case "BulletBossBT":
                targetPool = bulletBossBT;
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
            case "PlayerChargeBullet":
                targetPool = PlayerChargeBullet;
                break;
            case "PlayerMeleeHit":
                targetPool = PlayerMeleeHit;
                break;
            case "PlayerRangeHit":
                targetPool = PlayerRangeHit;
                break;
            case "PlayerJumpHit":
                targetPool = PlayerJumpHit;
                break;
            case "PlayerDeflectEffect":
                targetPool = PlayerDeflectEffect;
                break;
            case "enemyB":
                targetPool = enemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "enemyBT":
                targetPool = enemyBT;
                break;
            case "BulletBossBT":
                targetPool = bulletBossBT;
                break;
        }

        return targetPool;
    }

}
