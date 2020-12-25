using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMenu : MonoBehaviour
{
    //game initialize
   public void PlayGame ()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
   }
    //game exiting
   public void QuitGame(){
       Application.Quit();
   }
}
