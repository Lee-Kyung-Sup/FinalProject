using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEventChecker : MonoBehaviour
{
    public Dictionary<GameObject, bool> isClear = new Dictionary<GameObject, bool>();
    public Dictionary<GameObject, bool> isBroken = new Dictionary<GameObject, bool>();
}
