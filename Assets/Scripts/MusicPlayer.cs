using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField] 
    private AudioClip winMusic;
    
    [SerializeField] 
    private AudioClip backgroundMusic;
    
    // Use this for initialization
    void Awake () {
        SetUpSingleton();
        _audioSource = GetComponent<AudioSource>();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayWinMusic()
    {
        _audioSource.clip = winMusic;
        _audioSource.Play();
        
    }
    
    public void PlayBackgroundMusic()
    {
        _audioSource.clip = backgroundMusic;
        _audioSource.Play();
        
    }
    
}