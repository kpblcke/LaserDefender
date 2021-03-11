using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour {

    [SerializeField] private GameObject livesShow;
    private List<GameObject> livesObject;
    GameSession _gameSession;

    // Use this for initialization
    void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
        livesObject = new List<GameObject>();
        for (int i = 0; i < _gameSession.GetLives(); i++)
        {
            livesObject.Add(Instantiate(livesShow, transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShowActualLives();
    }

    private void ShowActualLives()
    {
        if (_gameSession.GetLives() != livesObject.Count)
        {
            if (_gameSession.GetLives() < livesObject.Count)
            {
                for (int i = _gameSession.GetLives(); i < livesObject.Count; i++)
                {
                    Destroy(livesObject[i]);
                    livesObject.RemoveAt(i);
                }
            }
            else
            {
                for (int i = livesObject.Count; i < _gameSession.GetLives(); i++)
                {
                    livesObject.Add(Instantiate(livesShow, transform));
                }
            }
        }
    }
}