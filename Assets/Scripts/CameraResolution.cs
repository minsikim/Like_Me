using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    [SerializeField]
    private bool _keepResolution = true;
    float defaultAspectRatio = 9f / 16f;
    float defaultWidth;

    void Start()
    {
        defaultWidth = 7f * defaultAspectRatio;
    }

    void Update()
    {
        if (_keepResolution)
        {
            Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;
            
            float currentAspectRatio = Camera.main.aspect;
            float newCameraHeight = (8.23f * currentAspectRatio / defaultAspectRatio);
            var tempPosition = Camera.main.transform.position;
            Camera.main.transform.position = new Vector3(tempPosition.x, newCameraHeight - 7.23f, tempPosition.z);
        }
    }
}
