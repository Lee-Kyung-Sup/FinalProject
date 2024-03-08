using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="mapData")]
public class MapData : ScriptableObject
{
    public Maps[] mapData;
}
[Serializable]
public class Maps
{
    public GameObject maps;
    public Poter[] poter;
}
[Serializable]
public class Poter
{
    public int goMapIndex;
    public Vector3 pos;
    public Vector3 targetPos;
    public Vector2 size;
}
