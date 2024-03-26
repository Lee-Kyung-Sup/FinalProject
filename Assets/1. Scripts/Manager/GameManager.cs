using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UI;




public enum CharacterType
{
    Duck,
    Unknown
}

[System.Serializable]
public class Character
{
    public CharacterType CharacterType;
    public Sprite CharacterSprite;
    public RuntimeAnimatorController AnimatorController;

}




public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject player; // 플레이어 오브젝트 위치 추적
    public ObjectManager objectManager;
    //public Transform playerPosition;

    public TextAsset ItemDatabase;



    PlayerUI playerUI;

    public List<Character> CharacterList = new List<Character>();

    public Animator PlayerAnimator;
    public TMP_Text PlayerName;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //player = GameObject.FindGameObjectWithTag("Player");
        objectManager = GetComponent<ObjectManager>();

    }

    // Start is called before the first frame update
    void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Vector3 GetPlayerPosition() // 플레이어 위치 추적용
    {
        return player.transform.position;
    }

    public GameObject GetPlayerObject()
    {
        return player.gameObject;
    }

    public void SetCharacter(CharacterType characterType, string name)
    {
        var character = GameManager.instance.CharacterList.Find(item => item.CharacterType == characterType);

        //PlayerAnimator.runtimeAnimatorController = character.AnimatorController; //애니메이터 안 넣으면 게임작동 안할 수 있음
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
