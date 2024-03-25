using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Gfour : MonoBehaviour
{
    LayerMask gLyaer;
    RaycastHit2D up;
    RaycastHit2D down;
    private void Start()
    {
        gLyaer = LayerMask.GetMask("Ground");
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.up * 0.5f, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * 1.45f, Color.red);

        up = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, gLyaer);
        down = Physics2D.Raycast(transform.position, Vector2.down, 1.45f, gLyaer);
        if (up.transform != null && down.transform != null)
        {
            //Todo Die
        }
    }
}
