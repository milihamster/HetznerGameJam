using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; } 

    public Transform SpawnAreaSea;
    public Transform SpawnAreaLand;
    public Transform SpawnAreaSky;

    public int EnemyMaxCount;
    public int PlanctonSpawnAmount;
    public float PlanctonSpawnRate;

    public SpawnManager()
    {
        Instance = this;
    }

    void Start()
    {
        RespawnPlayer();
        InitializeEnemies();
        InvokeRepeating("SpawnPlanctons", 0, PlanctonSpawnRate);
    }

    public void RespawnPlayer()
    {
        var animal = SpawnAnimal(GlobalDataSo.Instance.Animals.First(x => x.Level == 1).Prefab, SpawnAreaSea, false);
        animal.GetComponent<ControlsAiFish>().enabled = false;
        animal.GetComponent<ControlsPlayer>().enabled = true;

        animal.name = "PICKME!!!!";

        CameraController.Instance.Target = animal.transform;
    }

    public void ReSpawnEnemy(Animal animal)
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

        var newForm = GlobalDataSo.Instance.Animals.FirstOrDefault(x => x.Level > animal.AnimalSo.Level).Prefab;

        //TODO: LevelUpAnimation

        Instantiate(newForm, spawnPosition, animal.transform.rotation);
        Destroy(animal.gameObject);
    }

    void InitializeEnemies()
    {
        for (int i = 0; i < EnemyMaxCount; i++)
        {
            SpawnAnimal(GlobalDataSo.Instance.Animals.First(x => x.Level == 1).Prefab, SpawnAreaSea, false);
        }
    }

    void SpawnPlanctons()
    {
        for (int i = 0; i < PlanctonSpawnAmount; i++)
        {
            SpawnAnimal(GlobalDataSo.Instance.Animals.First(x => x.Level == 0).Prefab, SpawnAreaSea, true);
        }
    }

    Animal SpawnAnimal(Animal animal, Transform area, bool grounded)
    {
        // Bereich
        float randomX = UnityEngine.Random.Range(-area.localScale.x, area.localScale.x)/2;
        float randomY = UnityEngine.Random.Range(-area.localScale.y, area.localScale.y)/2;
        Vector3 spawnPoint = new Vector3(randomX, randomY);
        if(grounded)
        {
            RaycastHit hit;
            if(Physics.Raycast(spawnPoint, -Vector3.up, out hit))
            {
                spawnPoint.y = hit.point.y;
            }
        }
        return Instantiate(animal.AnimalSo.Prefab, spawnPoint, animal.transform.rotation);
    }
}
