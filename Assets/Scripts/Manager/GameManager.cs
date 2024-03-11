using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance; //½Ì±ÛÅæ

    public Transform Player {  get; private set; }
    [SerializeField] private string playerTag = "Player";
    //private HealthSystem playerHealthSystem; //½½¶óÀÌ´õ Àû¿ëÄÚµå

    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private SliderJoint2D hpGaugeSlider;

    public static GameManager Instance
    {
        get {

            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if(_instance == null)
                {
                    GameObject gameManagerObject = new GameObject("GameManager");
                    _instance = gameManagerObject.AddComponent<GameManager>();
                }

            }
            return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Player = GameObject.FindGameObjectWithTag(playerTag).transform;

            //playerHealthSystem = Player.GetComponent<HealthSystem>();
            //playerHealthSystem.OnDamage += UpdateHealthUI;
            //playerHealthSystem.OnHeal += UpdateHealthUI;
            //playerHealthSystem.OnDeath += GameOver;

            //gameOverUI.SetActive(false);

            //DontDestroyOnLoad(gameObject); //¾À ÀüÈ¯½Ã ÆÄ±«¹æÁö
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    private void UpdateHealthUI()
    {
        //hpGaugeSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
    }

    private void GameOver()
    {
        //gameOverUI.SetActive(true);
    }

    private void UpdateWaveUI()
    {
        //waveText.text;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
