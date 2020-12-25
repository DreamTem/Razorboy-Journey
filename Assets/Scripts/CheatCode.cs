using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    public GameObject cheatText,cheatIm;
    public static bool cheatStop; 
    public bool  button1,button2,button3,button4,button5;
    // Start is called before the first frame update
    void Start()
    {
        cheatText.gameObject.SetActive(false);
        cheatIm.gameObject.SetActive(false);
        button1 = false;
        button2 = false;
        button3 = false;
        button4 = false;
        button5 = false;
        cheatStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        //button 1 press
        if (Input.GetKeyDown(KeyCode.JoystickButton0)){
            button1 = true;
        }
        //button 2 press
        if (Input.GetKeyDown(KeyCode.JoystickButton1)){
            button2 = true;
        }
        //button 3 press
        if (Input.GetKeyDown(KeyCode.JoystickButton2)){
            button3 = true;
        }
        //button 4 press
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            button4 = true;
        }
        //button 5 press
        if (Input.GetKeyDown(KeyCode.JoystickButton5)){
            button5 = true;
        }
        //button 1 unpress
        if (Input.GetKeyUp(KeyCode.JoystickButton0)){
            button1 = false;
        }
        //button 2 unpress
        if (Input.GetKeyUp(KeyCode.JoystickButton1)){
            button2 = false;
        }
        //button 3 unpress
        if (Input.GetKeyUp(KeyCode.JoystickButton2)){
            button3 = false;
        }
        //button 4 unpress
        if (Input.GetKeyUp(KeyCode.JoystickButton3))
        {
            button4 = false;
        }
        //button 5 unpress
        if(Input.GetKeyUp(KeyCode.JoystickButton5)){
            button5 = false;
        }
        //cheat code feature showing
        if(button1 && button2 && button3 && button4 && button5){
            cheatText.gameObject.SetActive(true);
            cheatIm.gameObject.SetActive(true);
            cheatStop = true;
            Time.timeScale = 0;
        }
    }
}
