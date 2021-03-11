using UnityEngine;

[CreateAssetMenu(fileName = "LaserGun", menuName = "Gun", order = 0)]
public class LaserGunConfig : ScriptableObject
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float minTimeBetweenShots = 0.1f;
    [SerializeField] float maxTimeBetweenShots = 0.2f;
    [SerializeField] private bool autoFire = false;
    [SerializeField] private bool playerGun = false;
    
    [Header("Sound")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    public GameObject LaserPrefab => laserPrefab;

    public float ProjectileSpeed => playerGun ? projectileSpeed : -projectileSpeed;

    public float MINTimeBetweenShots => minTimeBetweenShots;

    public float MAXTimeBetweenShots => maxTimeBetweenShots;

    public bool AutoFire => autoFire;

    public AudioClip ShootSound => shootSound;

    public float ShootSoundVolume => shootSoundVolume;
}
