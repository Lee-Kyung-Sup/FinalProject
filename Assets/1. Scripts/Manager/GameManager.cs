using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public Sprite CharacterSprite;
}

public class GameManager : SingletonBase<GameManager>
{
    public GameObject player;
    public PlayerUI playerUI;
    public Animator PlayerAnimator;
    public TMP_Text PlayerName;

    public ObjectManager objectManager;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        objectManager = GetComponent<ObjectManager>();
    }
    private void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
    }

    public Vector3 GetPlayerPosition() // 플레이어 위치 추적용
    {
        return player.transform.position;
    }

    public GameObject GetPlayerObject()
    {
        return player.gameObject;
    }

    public void SetCharacter(string name)
    {
        PlayerName.text = name;
    }

    public void OnGameOver()
    {
        // 게임 오버 
        Invoke("InvokeGameOver", 1.5f);
    }

    private void InvokeGameOver()
    {
        playerUI.OnGameOverUI();

        Time.timeScale = 0;
    }
}
