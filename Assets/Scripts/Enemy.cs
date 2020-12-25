using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private  int health=3;

     void Start()
    {
        
        anim = GetComponent<Animator>();
        gameObject.SetActive(true);
        health = 3;
      
    }
    void Update()
    {
        //death
        if (health < 0)
        {
            Destroy(gameObject);
        }

    }
    //arm attack to the melee enemy
    public void TakeDamage(int playerDamage)
    {
        EnemyAttack.canDamage = false;
        health -= playerDamage;
        anim.SetTrigger("DmgTake");
    }
}
