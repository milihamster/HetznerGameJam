using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI Code
/// </summary>
public abstract class ControlsAi : Controls
{
    public static bool AllowAttack = true;

    protected Animal _animal;

    void Start()
    {
        _animal = GetComponent<Animal>();
        _animal.OnDeath.AddListener(() =>
            SpawnManager.Instance.RespawnAnimal(_animal));
    }
}
