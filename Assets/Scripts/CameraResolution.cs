using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds
{
    public static float xMax { get; set; }
    public static float xMin { get; set; }
    public static float yMax { get; set; }
    public static float yMin { get; set; }

}


public class CameraResolution : MonoBehaviour
{
    [SerializeField]
    private bool _keepResolution = true;
    float defaultAspectRatio = 9f / 16f;
    float defaultWidth;

    private void Awake()
    {
        Bounds.xMin = -25f;
        Bounds.xMax = 25f;
        Bounds.yMin = -10f;
        Bounds.yMax = 40f;
    }

    void Start()
    {
        defaultWidth = (Bounds.xMax - Bounds.xMin) * defaultAspectRatio * 0.9f;
        Debug.Log(Camera.main.aspect);
    }
    private void Update()
    {
        if (_keepResolution)
        {
            Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;

            float currentAspectRatio = Camera.main.aspect;
            float newCameraHeight = (((Bounds.yMax - Bounds.yMin) * 1.15f) * currentAspectRatio / defaultAspectRatio) - (Bounds.yMax - Bounds.yMin);
            var tempPosition = Camera.main.transform.position;
            Camera.main.transform.position = new Vector3(tempPosition.x, newCameraHeight, tempPosition.z);
        }
    }
}
