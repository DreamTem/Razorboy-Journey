using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartingWhileGame : MonoBehaviour
{
    public static bool stoped;
    public AudioSource main;
    //restarting
    public void Restart()
    {
        main.volume=0.2f;
        Time.timeScale=1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //exit to main menu
    public void Exiting()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-SceneManager.GetActiveScene().buildIndex);
    }
}
