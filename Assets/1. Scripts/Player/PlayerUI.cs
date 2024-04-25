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
    public void UpdateStaminaUI(float stamina)
    {
        staminaUI.value = stamina;
    }

    public void OnGameOverUI()
    {
        gameOverUI.SetActive(true);
        AudioManager.Instance.PlaySFX("GameOver");
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        gameOverUI.SetActive(false);
        SceneManager.LoadScene("2. GameScene");
        MapMaker.Instance.StartMake();
    }
}


