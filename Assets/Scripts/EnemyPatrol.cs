using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    private Animator anim;
    public static bool movingRight,movingLeft;    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        movingRight =true;
        movingLeft = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //left moving
        if(movingRight==true&&EnemyAttack.AIStop==false&&EnemyAttack.canDamage==true){
            transform.Translate(Vector2.right*speed*Time.deltaTime);
           transform.localScale=new Vector2(0.762773f,0.762773f);
        }
        //right moving
        else if(movingLeft==true && EnemyAttack.AIStop == false && EnemyAttack.canDamage == true)
        {
            transform.Translate(Vector2.left*speed*Time.deltaTime);
            transform.localScale=new Vector2(-0.762773f,0.762773f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //left move change
        if(other.gameObject.CompareTag("LeftMove"))
        {
            movingRight=false;
            movingLeft = true;
        }
        //right move change
        if(other.gameObject.CompareTag("RightMove"))
        {
            movingRight=true;
            movingLeft = false;
        }
    }
}
