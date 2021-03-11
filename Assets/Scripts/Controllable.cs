using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Controllable : MonoBehaviour
{
    [SerializeField] private float possibleMoveSpeed = 10f;
    [SerializeField] private AudioClip getInSound;
    [SerializeField] private List<Component> removeThis;
    [SerializeField] [Range(0f,2f)] private float stabilizeAfter = 2f;
    
    private float currentTime = 0f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Movable movable = other.gameObject.GetComponent<Movable>();
        if (movable)
        {
            GetInto(movable);
        }
    }

    private void GetInto(Movable movable)
    {
        foreach (var component in removeThis)
        {
            Destroy(component);
        }
        
        StartCoroutine(Stabilize());
        AudioSource.PlayClipAtPoint(getInSound, Camera.main.transform.position, 1f);
        Movable newMove = gameObject.AddComponent<Movable>();
        newMove.SetMoveSpeed(possibleMoveSpeed);
        Destroy(movable.gameObject);
    }

    IEnumerator Stabilize()
    {
        do
        {
            currentTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, currentTime/stabilizeAfter);
            yield return new WaitForEndOfFrame();
        } while (currentTime < stabilizeAfter);
        transform.rotation = Quaternion.identity;
        
    }

}
