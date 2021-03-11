using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    [SerializeField] float delayInSeconds = 2f;
    
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Game");
    }

    public void WinLevel()
    {
        FindObjectOfType<MusicPlayer>().PlayWinMusic();
        StartCoroutine(WaitAndLoad("Win Game"));
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("Game over"));
    }

    IEnumerator WaitAndLoad(string sceneName)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}