using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "potal")]
[Serializable]
public class PotalData : ScriptableObject
{
    public int id;
    public Vector3 pos;
    public Vector3 targetPos;
}
