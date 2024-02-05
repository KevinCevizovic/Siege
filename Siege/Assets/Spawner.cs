using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject prefab;

    public float spawnTime = 15f;
    public Transform[] spawnPoints;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(prefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        enemies.Add(prefab);

    }

    void Update()
    {
        
    }

    // stop spawning when castle 
}
