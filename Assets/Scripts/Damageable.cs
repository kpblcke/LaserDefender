using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private GameObject damageVFX;
    [SerializeField] private AudioClip onDestroySound;
    [SerializeField] [Range(0f, 1f)] private float soundVolume = 0.7f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            GameObject damageParticle = Instantiate(damageVFX, other.transform.position, transform.rotation);
            Destroy(damageParticle, 1f);
            damageDealer.Hit();
        }
    }
}
