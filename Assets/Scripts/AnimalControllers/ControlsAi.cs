using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI Code
/// </summary>
public abstract class ControlsAi : Controls
{
    void Start()
    {
        var animal = GetComponent<Animal>();
        animal.OnDeath.AddListener(() =>
            SpawnManager.Instance.RespawnAnimal(animal));
    }
}
