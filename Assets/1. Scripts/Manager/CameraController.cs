using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SingletonBase<CameraController>
{
    Camera _camera;
    WaitForSeconds mapMoveDark = new WaitForSeconds(0.2f);

    Transform target;
    Vector3 cameraPos;

    public BoxCollider2D CameraArea { get; private set; }
    Vector3 minArea;
    Vector3 maxArea;
    float cameraHalfHeight;
    float cameraHalfWidth;
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    private void Start()
    {
        target = GameManager.Instance.Player.transform;
    }

    private void LateUpdate()
    {
        CameraMove();
    }
    public void SetCameraArea(BoxCollider2D newArea)
    {
        CameraArea = newArea;
        minArea = CameraArea.bounds.min;
        maxArea = CameraArea.bounds.max;
        cameraHalfHeight = _camera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Screen.width / Screen.height;
    }
    void CameraMove() //ToDo
    {
        float moveSpeed = Vector2.Distance(target.position,transform.position);
        cameraPos.Set(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, cameraPos, moveSpeed * Time.deltaTime);
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
}
