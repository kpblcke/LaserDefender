using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    [SerializeField] private float shieldTime = 4f;
    [SerializeField] private GameObject shieldPref;
    [SerializeField] private float minSpeed = 2f;
    [SerializeField] private float maxSpeed = 6f;
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
            float invicibleTime = Random.Range(0f, shieldTime);
            GameObject shield = Instantiate(shieldPref, transform.position, transform.rotation);
            shield.GetComponent<FollowTransform>().SetFollowTransform(player.transform);
            Destroy(shield, invicibleTime);
            AudioSource.PlayClipAtPoint(soundOnGetUp, Camera.main.transform.position, soundVolume);
            Destroy(gameObject);
        }
    }
}