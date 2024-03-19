using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite redHeart;
    [SerializeField] private Sprite blackHeart;
    [SerializeField] private PlayerUI healthUI;
    public Slider staminaUI;


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

}


