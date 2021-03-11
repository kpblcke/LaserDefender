using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserGun : MonoBehaviour
{
    [SerializeField] private LaserGunConfig _gunConfig;
    [SerializeField] private bool canFire = false;
    
    Coroutine firingCoroutine;
    private bool firing = false;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        if (_gunConfig.AutoFire)
        {
            StartCoroutine(WaitAndStartFire(Random.Range(_gunConfig.MINTimeBetweenShots, _gunConfig.MAXTimeBetweenShots)));
        }
    }

    public void LostControll()
    {
        canFire = false;
    }

    public void GetControll()
    {
        canFire = true;
    }

    void Update () {
	    Fire();	
    }

    private void Fire()
    {
        if (_gunConfig.AutoFire || !canFire) return;
        
        if (Input.GetButtonDown("Fire1") && !firing)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1") && !Input.GetButton("Fire1") && firing)
        {
            StopCoroutine(firingCoroutine);
            firing = false;
        }
    }

    IEnumerator WaitAndStartFire(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(FireContinuously());
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            firing = true;
            GameObject laser = Instantiate(_gunConfig.LaserPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_gunConfig.ShootSound, _camera.transform.position, _gunConfig.ShootSoundVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _gunConfig.ProjectileSpeed);
            yield return new WaitForSeconds(Random.Range(_gunConfig.MINTimeBetweenShots, _gunConfig.MAXTimeBetweenShots));
        }
    }
}
