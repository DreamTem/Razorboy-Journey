using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPatrol : MonoBehaviour
{
    public float speed;
    private Animator anim;
    public static bool movingRight,movingLeft;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        movingRight = true;
        movingLeft = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //left moving
        if (movingLeft == true&&Shot.AIStop==false&&Shot.canDamage==true)
        {
            transform.Translate(Vector2.left*speed*Time.deltaTime);
            transform.localScale = new Vector2(-1, 1);
        }
        //right moving
        if (movingRight == true && Shot.AIStop == false && Shot.canDamage == true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.localScale = new Vector2(1, 1);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //left moving change
        if (other.gameObject.CompareTag("LeftMove"))
        {
            movingRight = false;
            movingLeft = true;
        }
        //rgiht moving change
        if (other.gameObject.CompareTag("RightMove"))
        {
            movingRight = true;
            movingLeft = false;
        }
    }
}
