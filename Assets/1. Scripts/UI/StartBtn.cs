using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    void Update()
    {

    }

    public void StartGame()
    {
            SceneManager.LoadScene("2. GameScenes");
    }

}
