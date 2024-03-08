using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController i;
    Camera _camera;
    WaitForSeconds mapMoveDark = new WaitForSeconds(0.2f);
    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    public void CameraOFFON()
    {
        StartCoroutine(CameraOFF());
    }
    IEnumerator CameraOFF()
    {
        _camera.cullingMask = ~-1;
        yield return mapMoveDark;
        _camera.cullingMask = -1;
    }
}
