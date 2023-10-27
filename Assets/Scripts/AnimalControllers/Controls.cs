using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for Controls
/// This class consists of values that'll be used by AI or Players to interact with Animals.
/// </summary>
public abstract class Controls : MonoBehaviour
{
    public float MovementHorizontal { get; protected set; }
    public float MovementVertical { get; protected set; }
    public bool Attack { get; protected set; }
    public bool Special { get; protected set; }

    // Update is called once per frame
    void Update()
    {
        HandleControls();
    }

    protected abstract void HandleControls();
}
