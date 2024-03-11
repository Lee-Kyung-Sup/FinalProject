using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="mapData")]
public class MapDatas : ScriptableObject
{
    public Map[] mapData;
}
[Serializable]
public class Map
{
    public GameObject maps;
    public Poter[] poter;
}
[Serializable]
public class Poter
{
    public int goMapIndex;
    public Vector3 potalPos;
    public Vector3 targetPos;
    public Vector2 potalSize;
}
