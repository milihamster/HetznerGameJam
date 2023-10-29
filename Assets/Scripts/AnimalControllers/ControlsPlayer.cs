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
        animal.OnDeath.AddListener(() => SpawnManager.Instance.RespawnPlayer());

        //var canvasExperience = Instantiate(GlobalDataSo.Instance.PrefabCanvasExperience.gameObject, transform);
        //animal.OnExperience.AddListener(() => canvasExperience.GetComponent<UiCanvasExperience>()
        //    .SetValue(animal.Experience, animal.AnimalSo.XpUntilLevelup));
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
