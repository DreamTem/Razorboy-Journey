using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    public GameObject ElevDoor,ElevDoor2;
    public static bool up, down,canUp,fals,canDown;
    // Start is called before the first frame update
    private void Start()
    {
        fals = false;
        canUp = true;
        canDown = false;
        ElevDoor.SetActive(false);
        ElevDoor2.SetActive(false);
        up = false;
        down = false;
    }

    private void Update()
    {
        //up moving for gamepad
        if(MoveFox.ELEV==true&&canUp==true&&Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            up=true;
            down=false;
        }
        //donw moving for gamepad
        else if(MoveFox.ELEV==true&&canDown==true&&Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            up=false;
            down=true;
        }
    }
    private void FixedUpdate()
    {
        //idle
        if (fals == false)
        {
            ElevDoor.SetActive(false);
            ElevDoor2.SetActive(false);
            CancelInvoke();
        }
        else
        {
            ElevDoor.SetActive(true);
            ElevDoor2.SetActive(true);
        }

        //up direction
        if (up == true)
        {
            fals = true;
            down = false;
            InvokeRepeating("Up", 0.2f, 0.2f);
            canDown = false;
            canUp = false;
        }
        //down direction
        if (down == true)
        {
            fals = true;
            up = false;
            InvokeRepeating("Down", 0.2f, 0.2f);
            canDown = false;
            canUp = false;
        }
    }
    //up moving
    private void Up()
    {
       transform.Translate(0,0.003f,0);
    }
    //down moving
    private void Down()
    {
        transform.Translate(0, -0.003f,0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //auto stoping
        if (other.gameObject.CompareTag("ElevatStop") && up)
        {
            CancelInvoke();
            canUp = false;
            canDown = true;
            up = false;
            down = false;
            fals = false;
        }
        //auto stoping 2
        if (other.gameObject.CompareTag("ElevatStop") && down)
        {
            CancelInvoke();
            canUp = true;
            canDown = false;
            up = false;
            down = false;
            fals = false;
        }
    }
}
