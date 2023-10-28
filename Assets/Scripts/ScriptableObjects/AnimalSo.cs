using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Balancing/Animal")]
public class AnimalSo : ScriptableObject
{
    public string Title;
    public string Description;
    public AnimalType Type;
    public Animal Prefab;

    public float Level;
    public int XpUntilLevelup;
}
