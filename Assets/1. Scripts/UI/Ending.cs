using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject congratmention;
    public GameObject credit;

    private void Start()
    {
        Invoke("congratsMent", 2f);
        Invoke("congratsCancel", 5f);
        Invoke("endCredit", 10f);
        Invoke("introScene", 23f);
    }

    void congratsMent()
    {
        congratmention.SetActive(true);
    }
    void congratsCancel()
    {
        congratmention.SetActive(false);
    }
    void endCredit()
    {
        credit.SetActive(true);
    }

    void introScene()
    {
        SceneManager.LoadScene("1. IntroScene");

    }

    






}
