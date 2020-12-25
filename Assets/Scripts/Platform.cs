using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool drawDebugRaycasts = true;
    private bool up, down,canChange;
    public float grabDistance, eyeHeight;
    int direction = 1;
    public float footOffset;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        canChange = true;
        up = true;
        down = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheatCode.cheatStop == false&&RestartingWhileGame.stoped==false)
        {
            Vector2 grabDir = new Vector2(direction, 0f);
            //right blank field raycast
            RaycastHit2D rightCheck = Raycast(new Vector2(footOffset * direction, eyeHeight), grabDir, grabDistance);

            //left blank field raycast
            RaycastHit2D leftCheck = Raycast(new Vector2(-3.5f * direction, eyeHeight), grabDir, grabDistance);

            //direction change check, when raycasts are inactive
            if (rightCheck || leftCheck)
            {
                canChange = true;
            }

            //direction change
            if (rightCheck == false && leftCheck == false&&canChange==true)
            {
                if (up == true)
                {
                    down = true;
                    up = false;
                    canChange = false;
                    return;
                }
                if (down == true)
                {
                    up = true;
                    down = false;
                    canChange = false;
                    return;
                }
            }

            //up moving
            if (up == true)
            {
                transform.Translate(new Vector2(0, 0.05f));
            }

            //down moving
            if (down == true)
            {
                transform.Translate(new Vector2(0f, -0.1f));
            }
        }
    }
    //raycast creating
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        //Call the overloaded Raycast() method using the ground layermask and return 
        //the results
        return Raycast(offset, rayDirection, length, groundLayer);
    }
    //main raycast example
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        //Record the player's position
        Vector2 pos = transform.position;

        //Send out the desired raycasr and record the result
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        //If we want to show debug raycasts in the scene...
        if (drawDebugRaycasts)
        {
            //...determine the color based on if the raycast hit...
            Color color = hit ? Color.red : Color.green;
            //...and draw the ray in the scene view
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }

        //Return the results of the raycast
        return hit;
    }
    
}
