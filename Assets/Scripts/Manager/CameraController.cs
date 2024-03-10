using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController i;
    Camera _camera;
    WaitForSeconds mapMoveDark = new WaitForSeconds(0.2f);

    GameObject player;
    Vector3 target;
    float moveSpeed = 4;

    BoxCollider2D cameraArea;
    Vector3 minArea;
    Vector3 maxArea;
    float cameraHalfHeight;
    float cameraHalfWidth;
    private void Awake()
    {
        i = this;
        _camera = GetComponent<Camera>();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }
    private void Update()
    {
        target.Set(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
        float clampedX = Mathf.Clamp(transform.position.x, minArea.x + cameraHalfWidth, maxArea.x - cameraHalfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minArea.y + cameraHalfHeight, maxArea.y - cameraHalfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

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
    public void SetCameraArea(BoxCollider2D newArea)
    {
        cameraArea = newArea;
        minArea = cameraArea.bounds.min;
        maxArea = cameraArea.bounds.max;
        cameraHalfHeight = _camera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Screen.width / Screen.height;
    }
}
