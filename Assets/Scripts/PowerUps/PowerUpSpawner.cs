using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerUps;
    [SerializeField] private float minSpawnTime = 10f;
    [SerializeField] private float maxSpawnTime = 25f;
    [SerializeField] private float yOffset = 8.2f;
    [SerializeField] private float minX = -3.7f;
    [SerializeField] private float maxX = 3.7f;

    private void Start()
    {
        if (powerUps != null && powerUps.Count > 0)
        {
            StartCoroutine(SpawnPowerUps());
        } 
        
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            Instantiate(powerUps[Random.Range(0, powerUps.Count)],
                new Vector3(Random.Range(minX, maxX), yOffset, 0f), 
                Quaternion.identity);
            
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }

    }
}
