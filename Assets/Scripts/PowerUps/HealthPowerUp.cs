using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthPowerUp : MonoBehaviour
{
    [SerializeField] private int restoreHealth = 100;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private AudioClip soundOnGetUp;
    [SerializeField] [Range(0,1f)] private float soundVolume = 1f;
    
    private Rigidbody2D _rigidbody2D;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(0f, -Random.Range(minSpeed, maxSpeed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            player.AddHealth(restoreHealth);
            AudioSource.PlayClipAtPoint(soundOnGetUp, Camera.main.transform.position, soundVolume);
            Destroy(gameObject);
        }
    }
}