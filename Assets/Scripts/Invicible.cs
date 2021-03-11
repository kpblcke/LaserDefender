using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Invicible : MonoBehaviour
{
    [SerializeField] [Range(0, 4f)] private float invicibleMaxTime;
    [SerializeField] private GameObject shieldPref;
    private float invicibleTime;
    private void Start()
    {
        invicibleTime = Random.Range(0f, invicibleMaxTime);
        GameObject shield = Instantiate(shieldPref, transform.position, transform.rotation);
        shield.GetComponent<FollowTransform>().SetFollowTransform(transform);
        Destroy(shield, invicibleTime);
        Destroy(this, invicibleTime);
    }
}
