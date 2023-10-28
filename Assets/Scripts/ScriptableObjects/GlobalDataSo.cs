using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Balancing/GlobalData")]
public class GlobalDataSo : ScriptableObject
{
    private static GlobalDataSo _instance;

    public static GlobalDataSo Instance
    {
        get
        {
            _instance = Resources.Load<GlobalDataSo>("GlobalData");
            return _instance;
        }
    }

    public List<AnimalSo> Animals = new();
    public ParticleSystem PrefabDefaultDeathEffect;
    public UiCanvasExperience PrefabCanvasExperience;
}
