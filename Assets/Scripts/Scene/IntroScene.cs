using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.ShowUI<UILogin>();
    }
}
