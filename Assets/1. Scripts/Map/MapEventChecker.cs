using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEventChecker : MonoBehaviour
{
    public Dictionary<string, bool> isClear = new Dictionary<string, bool>(10);
    public Dictionary<string, bool> isBroken = new Dictionary<string, bool>(10);
}
