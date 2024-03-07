using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        UserManager.Instance.LoadItem();
        UIManager.Instance.ShowUI<UIInventory>();
    }
}
