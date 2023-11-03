using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// TODO: Fix weird blue flash
public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Transform Target;

    public Camera Camera;

    public CameraController()
    {
        Instance = this;
    }

    void Awake()
    {
        Camera = GetComponentInChildren<Camera>();

        // Automatically choose Player as Target if there isn't any
        if (Target == null)
        {
            var targets = Transform.FindObjectsOfType<ControlsPlayer>();
            Target = targets.FirstOrDefault(x => x.isActiveAndEnabled)?.transform; 
        }
    }

    public void SetCameraSize(float newSize, float time = 1f, Action callback = null)
    {
        LeanTween.cancel(Camera.gameObject);
        LeanTween.value(Camera.gameObject, Camera.orthographicSize, newSize, time)
            .setOnUpdate((float val) =>
            {
                Camera.orthographicSize = val;
            })
            .setOnComplete(() => callback?.Invoke())
            .setEaseInOutSine();
    }

    void Update()
    {
        // If Target is far away:
        // Move camera there smoothly
        if(!LeanTween.isTweening(gameObject) && Target != null)
        {
            var distance = Vector2.Distance(
                new(transform.position.x, transform.position.y),
                new(Target.position.x, Target.position.y));
            if (distance > 3)
            {
                LeanTween.move(gameObject, Target.position, 1f)
                    .setEaseOutSine();
            }
            else if(distance > 0.75f)
            {
                LeanTween.value(gameObject, 0, 1, 0.25f)
                    .setOnUpdate((float percentage) => {
                        transform.position = Vector3.Lerp(transform.position, Target.position, percentage);
                    });
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
