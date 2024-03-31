using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoad : MonoBehaviour
{
    public Slider progressbar;
    public TMP_Text loadtext;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("2. GameScenes");
        operation.allowSceneActivation = false;
 
        while (!operation.isDone)
        {
                yield return null;
                if (progressbar.value < 0.9f)
                {
                    progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
                }
                else if (progressbar.value >= 0.9f)
                {
                    progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
                }


                if(progressbar.value >= 1f)
                {
                    loadtext.text = "Press SpaceBar";
                }

                if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }
        }
    }
}
