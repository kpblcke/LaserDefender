using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] private bool looping = true;
    [SerializeField] private bool bossPhase = false;
    [SerializeField] private bool bossDefeated = false;
    [SerializeField] private GameObject bossPref;
    private GameObject currentBoss;
    
    // Use this for initialization
    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnLoop());
    }

    public void BossIncoming()
    {
        bossPhase = true;
        if (currentBoss is null)
        {
            currentBoss = Instantiate(bossPref);
        }
    }

    public void BossDead()
    {
        bossPhase = false;
        bossDefeated = true;
    }

    private IEnumerator SpawnLoop()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping || !bossPhase);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }


    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            if (bossPhase)
            {
                yield break;
            }
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

}
