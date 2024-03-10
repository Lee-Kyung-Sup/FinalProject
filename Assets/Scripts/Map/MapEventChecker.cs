using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventChecker : MonoBehaviour
{
    public static MapEventChecker i;
    public Dictionary<GameObject, bool> isClear = new Dictionary<GameObject, bool>();
    private void Awake()
    {
        i = this;
    }
}
