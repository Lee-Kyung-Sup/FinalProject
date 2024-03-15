using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCount : MonoBehaviour
{
    [HideInInspector] public MapEvent mapEvent;
    private void OnDisable()
    {
        mapEvent.monsters--;
    }
}
