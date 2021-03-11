using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour {

	[Header("Player")]
	[SerializeField] int health = 200;
	[SerializeField] private GameObject deathVFX;
	[SerializeField] GameObject astronaut;
	[SerializeField] GameObject emptyShip;
	[SerializeField] private bool isControlled = true;
	
    [Header("Sound")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;

    private GameSession _gameSession;
    
    private void Start()
    {
	    _gameSession = FindObjectOfType<GameSession>();
	    HealthDisplay healthDisplay = FindObjectOfType<HealthDisplay>();
	    if (healthDisplay)
	    {
		    healthDisplay.NewPlayer(this);
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
	    _gameSession.RemoveLive();
	    GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
	    Destroy(explosion, 4f);
	    AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
	    
	    if (_gameSession.GetLives() < 1)
	    {
		    FindObjectOfType<Level>().LoadGameOver();
		    Destroy(gameObject);
	    }
	    else
	    {
		    RespawnShip();
	    }
    }

    private void RespawnShip()
    {
	    if (isControlled)
	    {
		    Instantiate(astronaut, transform.position, transform.rotation);
		    Instantiate(emptyShip, new Vector3(-3.5f, -8.5f, 0f), transform.rotation);
	    }
	    
	    Destroy(gameObject);
    }

    public int GetHealth()
    {
	    return health;
    }

    public void AddHealth(int addHealth)
    {
	    health += addHealth;
    }
}