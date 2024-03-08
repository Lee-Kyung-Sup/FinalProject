using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public int goIndex;
    public Vector3 targetPos;
    LayerMask isPlayer;
    private void Awake()
    {
        isPlayer = LayerMask.GetMask("Player");
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayer.value == (isPlayer.value | (1 << collision.gameObject.layer)))
        {
            CameraController.i.CameraOFFON();
            MapMaker.i.MakeRoom(goIndex);
            collision.transform.position = targetPos;
        }
    }
}
