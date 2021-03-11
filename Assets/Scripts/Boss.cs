using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    [SerializeField] float health = 5000;
    [SerializeField] int scoreValue = 2000;
    [SerializeField] float spawnCounter;
    [SerializeField] float minTimeBetweenSpawn = 0.2f;
    [SerializeField] float maxTimeBetweenSpawn = 1f;
    
    [SerializeField] List<WaveConfig> summonWaves;
    
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.75f;
    
    // Use this for initialization
    void Start () {
        spawnCounter = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }

    // Update is called once per frame
    void Update () {
        CountDownAndSpawn();
    }

    private void CountDownAndSpawn()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0f)
        {
            StartCoroutine(SpawnAllEnemiesInWave(summonWaves[Random.Range(0, summonWaves.Count)]));
            spawnCounter = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
        }
    }
    
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        FindObjectOfType<EnemySpawner>().BossDead();
        FindObjectOfType<Level>().WinLevel();
        
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);

        Destroy(gameObject);
    }

}