using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectLoader : MonoBehaviour
{
    //private void Awake()
    //{
    //    var manager = GameManager.Instance;
    //}

    void Awake()
    {
        GameManager existingManager = FindObjectOfType<GameManager>();

        if (existingManager == null)
        {
            GameObject gameManager = Resources.Load<GameObject>("InGameObjects/GameManager");
            if (gameManager != null)
            {
                Instantiate(gameManager);
            }
        }
    }
}
