using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private Animator animator;
    public GameObject resp;
    private bool chck;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        chck=false;
    }

    // Update is called once per frame
    void Update()
    {
        //check point activate
        if(chck)
        {
            animator.SetTrigger("P");
        }
    }
    public void  OnTriggerEnter2D(Collider2D other)
    {
        //colliding with player
        if(other.gameObject.CompareTag("Player"))
        {
            chck=true;
        }
    }
}
