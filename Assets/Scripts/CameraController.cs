using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO: Fix weird blue flash
public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Transform Target;

    private Camera _camera;

    public CameraController()
    {
        Instance = this;
    }

    void Start()
    {
        _camera = GetComponentInChildren<Camera>();

        // Automatically choose Player as Target if there isn't any
        if (Target == null)
        {
            var targets = Transform.FindObjectsOfType<ControlsPlayer>();
            Target = targets.FirstOrDefault(x => x.isActiveAndEnabled).transform; 
        }
    }

    public void SetCameraSize(float newSize)
    {
        _camera.orthographicSize = newSize;
    }

    void Update()
    {
        // If Target is far away:
        // Move camera there smoothly
        if(!LeanTween.isTweening(gameObject) && Target != null)
        {
            if (Vector2.Distance(
                new(transform.position.x, transform.position.y),
                new(Target.position.x, Target.position.y)) > 10)
            {
                LeanTween.move(gameObject, Target.position, 1f)
                    .setEaseOutSine();
            }
            else
                transform.position = new(
                    Target.position.x,
                    Target.position.y,
                    -10 // 0 means the camera won't show anything
                    );
        }
    }
}
