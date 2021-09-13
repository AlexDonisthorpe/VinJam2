using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private bool _scalingOut = false;
    private float _scaleValue;
    private float _scaleTarget = 0f;
    private float _timeSpentLerping = 0f;

    private float velocity = 0f;
    private Camera _camera;
    private void Start()
    {
        _camera = GetComponent<Camera>();
        GetComponent<Camera>().orthographicSize = 1;
    }

    private void Update()
    {
        if (_scalingOut)
        {
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, _scaleTarget, ref velocity , 1f);
            if (_camera.orthographicSize >= _scaleTarget)
            {
                _scalingOut = false;
                velocity = 0f;
            }
        }
    }

    public void ScaleOut()
    {
        float orthoSize = _camera.orthographicSize;
        _scaleTarget = orthoSize + 1;
        _scalingOut = true;
        Debug.Log(_scaleValue);

    }

}
