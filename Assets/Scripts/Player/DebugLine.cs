using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLine : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _line = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        Debug.DrawRay(_line, _camera.transform.forward * 100f);
    }
}
