using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Fix weird bliue flash
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
    }

    void Update()
    {
        // If Target is far away:
        // Move camera there smoothly
        if(!LeanTween.isTweening(gameObject))
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
