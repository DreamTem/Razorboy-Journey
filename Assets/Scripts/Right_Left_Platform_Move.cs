using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Left_Platform_Move : MonoBehaviour
{
    private  bool right, left,canChange;
    // Start is called before the first frame update
    void Start()
    {
        canChange = true;
        right = true;
        left = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheatCode.cheatStop == false&&RestartingWhileGame.stoped==false)
        {
            //left moving
            if (left == true)
            {
                transform.Translate(new Vector2(-0.08f,0));
            }

            //right moving
            if (right == true)
            {
                transform.Translate(new Vector2(0.08f, 0));
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
            //direction change
            if (other.gameObject.CompareTag("ElChng"))
            {
                canChange=true;
                if (left == true&&canChange==true)
                {
                    Rght();
                }
                else
                {
                   Lft();
                }
            }
    }
    //left direction change
    void Lft()
    {
        left = true;
        right = false;
        canChange = false;
        return;
    }
    //right direction change
    void Rght()
    {
        right = true;
        left = false;
        canChange = false;
        return;
    }
}
