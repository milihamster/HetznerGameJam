using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class provides keyboard or controller inputs to an Animal
/// </summary>
public class ControlsPlayer : Controls
{
    void Start()
    {
        var animal = GetComponent<Animal>();
        animal.OnDeath.AddListener(() => {
            Timer.Pause();

            CameraController.Instance.SetCameraSize(CameraController.Instance.Camera.orthographicSize + 2, 
                4, () => SpawnManager.Instance.RespawnPlayer());
        });

        UiCanvasExperience.Instance.SetValue(animal.Experience, 0);

        //var canvasExperience = Instantiate(GlobalDataSo.Instance.PrefabCanvasExperience.gameObject, transform);
        animal.OnExperience.AddListener(() => UiCanvasExperience.Instance
            .SetValue(animal.Experience, animal.AnimalSo.XpUntilLevelup));

        animal.OnDeath.AddListener(() => SoundController.Instance.PlaySoundEffect(animal.deathSound));

        gameObject.name = $"PLAYER: {name}";
    }

    protected override void HandleControls()
    {
        MovementVertical = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        MovementHorizontal = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
        Attack = Input.GetKeyDown(KeyCode.Space);
        Special = Input.GetKeyDown(KeyCode.LeftControl);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }
}
