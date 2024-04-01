using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject congratmention;
    public GameObject credit;
    public GameObject endingmention;

    private void Start()
    {
        Invoke("congratsMent", 2f);
        Invoke("congratsCancel", 5f);
        Invoke("endCredit", 10f);
        Invoke("endingMent", 25f);
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

    void endingMent()
    {
        endingmention.SetActive(true);
    }





}
