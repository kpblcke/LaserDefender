using UnityEngine;

public class EmptyShip : MonoBehaviour
{
    [SerializeField] private AudioClip getInSound;
    [SerializeField] private GameObject normalShip;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Astronaut astronaut = other.gameObject.GetComponent<Astronaut>();
        if (astronaut)
        {
            GetInto(astronaut);
        }
    }

    private void GetInto(Astronaut astronaut)
    {
        AudioSource.PlayClipAtPoint(getInSound, Camera.main.transform.position, 1f);
        Instantiate(normalShip, transform.position, Quaternion.identity);
        Destroy(astronaut.gameObject);
        Destroy(gameObject);
    }
    
}
