using TMPro;
using UnityEngine;


[System.Serializable]
public class Character
{
    public Sprite CharacterSprite;
}

public class GameManager : SingletonBase<GameManager>
{
    public GameObject Player { get {if (player == null) return player = GameObject.FindGameObjectWithTag("Player");
            else return player; } }
    private GameObject player;
    public PlayerUI PlayerUI { get {if (playerUI == null) return playerUI = FindObjectOfType<PlayerUI>();
            else return playerUI; } }
    private PlayerUI playerUI; 
    public Animator PlayerAnimator;
    public TMP_Text PlayerName;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {

    }


    public Vector3 GetPlayerPosition() // �÷��̾� ��ġ ������
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
        // ���� ���� 
        Invoke("InvokeGameOver", 1.5f);
    }

    private void InvokeGameOver()
    {
        PlayerUI.OnGameOverUI();

        Time.timeScale = 0;
    }
    [SerializeField] int startMapId;
    public void MapMake()
    {
        MapMaker.Instance.StartMake(startMapId);
    }
}
