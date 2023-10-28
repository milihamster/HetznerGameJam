using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Transform Target;

    public CameraController()
    {
        Instance = this;
    }

    void Update()
    {
        transform.position = new(
            Target.position.x,
            Target.position.y,
            -10 // 0 means the camera won't show anything
            );
    }
}
