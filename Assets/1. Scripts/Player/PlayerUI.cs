using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite redHeart;
    [SerializeField] private Sprite blackHeart;
    [SerializeField] private PlayerUI healthUI;
    [SerializeField] private GameObject gameOverUI;
    public Slider staminaUI;

    private void Start()
    {
        gameOverUI.SetActive(false);
    }


    public void UpdateHeartUI(int health)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = redHeart;
            }
            else
            {
                hearts[i].sprite = blackHeart;
            }
        }
    }

    public void OnGameOverUI()
    {
        gameOverUI.SetActive(true);
    }


    public void UpdateStaminaUI(float stamina)
    {
        staminaUI.value = stamina;
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScenes"); // 임시) 메인 게임 씬으로
        // 현재 '본인이 작업하는 씬'에서 위의 GameScenes으로 넘어갈 경우,
        // 작업하던 씬의 메인 카메라를 가져가기 때문에 메인 카메라 2개 중복으로 오류 발생함.
    }
}


