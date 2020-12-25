using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour
{

    public GameObject gameOver, player, rest, res, health0, health1, health2, health3, health4, health5,mainMenu,menuImage,stamina,mainCam;
    public static int health = 5;

    public AudioSource playerAudio,mainAudio;
    public Animator anim;
    public AudioClip death;
    
    // Start is called before the first frame update
    void Start()
    {
        RestartingWhileGame.stoped = false;
        health0.SetActive(false);
        health5.SetActive(true);
        health4.SetActive(false);
        health3.SetActive(false);
        health2.SetActive(false);
        health1.SetActive(false);
        rest.SetActive(false);
        health = 5;
        gameOver.SetActive(false);
    }
   
    // Update is called once per frame
    void Update()
    {
        //Menu escape
        if (Input.GetKeyDown(KeyCode.JoystickButton7) && !MoveFox.dialogStop)
        {
            MenuExit();
        }
        //5 hp
        if(health == 5)
        {
            player.SetActive(true);
            rest.SetActive(false);                      
            gameOver.SetActive(false);
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(false);
            health5.SetActive(true);
        }
        //4 hp
        if (health == 4)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(true); ;
            health5.SetActive(false);
        }
        //3 hp
        if (health == 3)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(true);
            health4.SetActive(false);
            health5.SetActive(false);
        }
        //2 hp
        if (health == 2)
        {
            health1.SetActive(false);
            health2.SetActive(true);
            health3.SetActive(false);
            health4.SetActive(false);
            health5.SetActive(false);
        }
        //1 hp
        if (health == 1)
        {
            health1.SetActive(true);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(false);
            health5.SetActive(false);
        }
        //death
        if (health < 1)
        {
            mainCam.SetActive(true);
            health0.SetActive(true);
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(false);
            health5.SetActive(false);
            rest.SetActive(true);
            player.SetActive(false);            
            gameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                playerAudio.PlayOneShot(death, 0.45f);
                Resp();
            }
        }
        
    }
    //respawning the player
    public  void Resp()
    {
        player.transform.position = res.transform.position;
        MoveFox.rn = false;
        MoveFox.running = false;
        MoveFox.falling = false;
        health = 5;
    }
    //resume method
    public void Resuming()
    {
        RestartingWhileGame.stoped = false;
        Time.timeScale = 1;
        mainAudio.volume = 0.2f;
        mainMenu.SetActive(false);
        menuImage.SetActive(false);
        stamina.SetActive(true);
    } 
    //menu escape method
    void MenuExit()
    {
        RestartingWhileGame.stoped = true;
        mainAudio.volume = 0;
        mainMenu.SetActive(true);
        menuImage.SetActive(true);
        stamina.SetActive(false);
        Time.timeScale = 0;
    }
}