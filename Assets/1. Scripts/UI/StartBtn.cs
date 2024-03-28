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

        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM("FirstChapter");
        SceneManager.LoadScene("2. GameScenes");
    }

}
