using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public AudioSource source;

    public void ChangeToLevel() 
    {
        source.Play();
        SceneManager.LoadScene("DemoLevel");
    }

    public void ChangeToMainMenu()
    {
        source.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangeToTutorial()
    {
        source.Play();
        SceneManager.LoadScene("HowToPlay");
    }

    public void QuitTheGame() {
        source.Play();
        Application.Quit();
    }
}
