using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SingletonBase<CameraController>
{
    Camera _camera;
    WaitForSeconds mapMoveDark = new WaitForSeconds(0.2f);

    Transform target;
    Vector3 cameraPos;

    BoxCollider2D cameraArea;
    Vector3 minArea;
    Vector3 maxArea;
    float cameraHalfHeight;
    float cameraHalfWidth;

    protected override void Awake()
    {
        base.Awake();
        _camera = GetComponent<Camera>();
    }
    private void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject.transform; //이후 게임 매니저로부터 모든게 만들어질때 같이 할당
    }
    private void Update()
    {
        CameraMove();
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
    public void CameraViewZone(Transform viewPos)
    {
        target = viewPos;
    }
    void CameraMove() // 약간 하단에 위치하게 y값 조절 need
    {
        float moveSpeed = Vector2.Distance(target.position,transform.position);
        cameraPos.Set(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, cameraPos, moveSpeed * Time.deltaTime);
        float clampedX = Mathf.Clamp(transform.position.x, minArea.x + cameraHalfWidth, maxArea.x - cameraHalfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minArea.y + cameraHalfHeight, maxArea.y - cameraHalfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
