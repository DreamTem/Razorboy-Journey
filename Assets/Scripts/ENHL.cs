using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENHL : MonoBehaviour
{
    private int helth=2;
    public Animator anim;
    
    // Start is called before the first frame update
    void Awake()
    {
        //2 hp
        helth = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //death
        if(helth<1)
        {
            gameObject.SetActive(false);
        }
    }
    //arm attack to shooting enemy
    public void TkeDamage(int damage)
    {
        Shot.canDamage = false;
        anim.SetTrigger("DmgTake");
        helth -= damage;
    }
}
