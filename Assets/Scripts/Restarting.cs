using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Restarting : MonoBehaviour
{
    //exiting to main menu
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
    }
}
