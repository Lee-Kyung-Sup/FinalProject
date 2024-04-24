using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneUI : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.OnFadeOut();
    }

}
