using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance { get; set; }

    public EffectsManager()
    {
        Instance = this;
    }

    public void SpawnEffect(GameObject effect, Transform origin)
    {
        Instantiate(effect, origin);
    }

    public void SpawnEffect(GameObject effect, Transform origin, Vector3 offset)
    {
        Vector3 newPosition = origin.position + offset;
        Instantiate(effect, newPosition, origin.rotation);
    }

    public void SpawnEffect(GameObject effect, Vector3 position)
    {
        Instantiate(effect, position, effect.transform.rotation);
    }
}
