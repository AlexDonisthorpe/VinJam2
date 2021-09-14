using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    [SerializeField] private float[] scaleTargets;
    
    private bool _scalingOut = false;
    private int _scaleTargetIndex = 0;
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
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, scaleTargets[_scaleTargetIndex], ref velocity , 1f);
        }
    }

    public void ScaleOut()
    {
        _scaleTargetIndex++;
        _scalingOut = true;
    }

}
