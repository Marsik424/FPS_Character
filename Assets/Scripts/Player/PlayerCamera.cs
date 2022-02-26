using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [Range(0,120)][SerializeField] private float _cameraFOV = 60; 
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private float _minY = -90;
    [SerializeField] private float _maxY = 90;

    private Vector2 rotation = Vector2.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _camera = GetComponentInChildren<Camera>();
        _camera.fieldOfView = _cameraFOV;
    }
    private void Update()
    {
        CameraMove();
    }
    private void CameraMove()
    {
        rotation.x = Input.GetAxis("Mouse X") * _cameraSpeed * Time.deltaTime;
        rotation.y -= Input.GetAxis("Mouse Y") * _cameraSpeed * Time.deltaTime;

        rotation.y = Mathf.Clamp(rotation.y, _minY, _maxY);
        _camera.transform.localEulerAngles = new Vector3(rotation.y, 0, 0);
        transform.rotation *= Quaternion.Euler(0, rotation.x, 0);
    }
}
