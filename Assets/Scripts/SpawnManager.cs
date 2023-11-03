using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[Serializable]
public class SpawnableEntry
{
    public AnimalSo AnimalSo;
    public int Amount;
}

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    public Transform SpawnAreaSea;
    public Transform SpawnAreaLand;
    public Transform SpawnAreaSky;

    public int EnemyMaxCount;
    public int PlanctonSpawnAmount;
    public float PlanctonSpawnRate;

    public List<SpawnableEntry> SpawnablesSea = new();
    public List<SpawnableEntry> SpawnablesLand = new();
    public List<SpawnableEntry> SpawnablesSky = new();

    public GameObject level;

    public SpawnManager()
    {
        Instance = this;
    }

    void Start()
    {
        RespawnPlayer();
        //InvokeRepeating("SpawnPlanctons", 0, PlanctonSpawnRate);

        SpawnInitial(SpawnablesSea, SpawnAreaSea);
        SpawnInitial(SpawnablesLand, SpawnAreaLand, true);
        SpawnInitial(SpawnablesSky, SpawnAreaSky);
    }

    public void SpawnInitial(List<SpawnableEntry> spawnables, Transform area, bool grounded = false)
    {
        foreach (var spawnable in spawnables)
        {
            var animalSo = spawnable.AnimalSo;
            var amount = spawnable.Amount;

            for (int i = 0; i < amount; i++)
            {
                SpawnAnimal(animalSo.Prefab, area, grounded);
            }
        }
    }

    public void RespawnPlayer(Vector3? overwritePosition = null)
    {
        // Hard return if there's already a player in level
        // not necessarily the cleanest fix, but hey
        var playerControls = Transform.FindObjectsOfType<ControlsPlayer>(false);
        if (playerControls.Any(x => x.enabled))
        {
            Debug.LogError($"Tried Respawning Player, but PlayerControls already exist in the Level: {playerControls.First(x => x.enabled)}");   
            return;
        }

        var animal = SpawnAnimal(GlobalDataSo.Instance.Animals.First(x => x.Level == 1).Prefab, SpawnAreaSea, false, overwritePosition);
        animal.GetComponent<ControlsAiFish>().enabled = false;
        animal.GetComponent<ControlsPlayer>().enabled = true;

        CameraController.Instance.Target = animal.transform;
        CameraController.Instance.SetCameraSize(animal.AnimalSo.cameraSize);

        Timer.Reset();
        Timer.Start();
    }

    public void RespawnAnimal(Animal animal)
    {
        Transform chosenArea;
        bool grounded = false;

        switch (animal.AnimalSo.Type)
        {
            case AnimalType.SEA:
                chosenArea = SpawnAreaSea;
                break;
            case AnimalType.LAND:
                grounded = true;
                chosenArea = SpawnAreaLand;
                break;
            case AnimalType.SKY:
                chosenArea = SpawnAreaSky;
                break;
            default:
                throw new Exception("Missing enemy type");
        }

        SpawnAnimal(animal, chosenArea, grounded);
    }

    public void LevelUp(Animal animal)
    {
        Vector3 spawnPosition = animal.transform.position;

        bool isPlayer = animal.GetComponent<ControlsPlayer>().enabled;

        var newForm = GlobalDataSo.Instance.Animals
            .FirstOrDefault(x => x.Level > animal.AnimalSo.Level).Prefab;

        //TODO: LevelUpAnimation

        var newAnimal = Instantiate(newForm, spawnPosition, animal.transform.rotation);

        if (isPlayer)
        {
            newAnimal.GetComponent<ControlsAi>().enabled = false;
            newAnimal.GetComponent<ControlsPlayer>().enabled = true;

            SoundController.Instance.PlaySoundEffect(newAnimal.AnimalSo.levelUpSound);

            CameraController.Instance.Target = newAnimal.transform;
            CameraController.Instance.SetCameraSize(newAnimal.AnimalSo.cameraSize);
        }

        Destroy(animal.gameObject);
    }

    //void InitializeEnemies()
    //{
    //    for (int i = 0; i < EnemyMaxCount; i++)
    //    {
    //        SpawnAnimal(GlobalDataSo.Instance.Animals.First(x => x.Level == 1).Prefab, SpawnAreaSea, false);
    //    }
    //}

    //void SpawnPlanctons()
    //{
    //    for (int i = 0; i < PlanctonSpawnAmount; i++)
    //    {
    //        SpawnAnimal(GlobalDataSo.Instance.Animals.First(x => x.Level == 0).Prefab, SpawnAreaSea, true);
    //    }
    //}

    public static float LowestX;

    Animal SpawnAnimal(Animal animal, Transform area, bool grounded, Vector3? overwritePosition = null)
    {
        // Bereich
        float randomX = UnityEngine.Random.Range(-area.localScale.x, area.localScale.x) / 2;
        float randomY = UnityEngine.Random.Range(-area.localScale.y, area.localScale.y) / 2;
        Vector3 spawnPoint = area.position + new Vector3(randomX, randomY);

        if (overwritePosition != null)
            spawnPoint = overwritePosition.Value;
        else
        {
            PolygonCollider2D worldCollider = level.GetComponent<PolygonCollider2D>();

            if (worldCollider.OverlapPoint(spawnPoint))
            {
                return SpawnAnimal(animal, area, grounded);
            }
        }

        if (grounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(spawnPoint, -Vector3.up, out hit))
            {
                spawnPoint.y = hit.point.y;
            }
        }
        return Instantiate(animal.AnimalSo.Prefab, spawnPoint, animal.transform.rotation);
    }
}
