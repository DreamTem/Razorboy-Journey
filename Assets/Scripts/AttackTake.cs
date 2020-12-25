using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTake : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        //anim set
        anim = GetComponent<Animator>();
    }
    public void Explode()
    {
        GameControlScript.health = 0;
    }
    //gamage from enemy's knife
    public void TakeDamage(int Damage)
    {
        if(!PlayerAttack.press)
        {
            PlayerAttack.canDamage = false;
            MoveFox.running = false;
            MoveFox.jmp = false;
            MoveFox.rn = false;
            anim.SetTrigger("OR");
            anim.SetTrigger("DmgTake");
            GameControlScript.health -= Damage;     
        }
       
    }
    //gamage from enemy's stronger knife
    public void TKDMG(int LeggDamage)
    {
        if(!PlayerAttack.press)
        {
            PlayerAttack.canDamage = false;
            MoveFox.running = false;
            MoveFox.jmp = false;
            MoveFox.rn = false;
            anim.SetTrigger("OR");
            anim.SetTrigger("DmgTake");
            GameControlScript.health -= LeggDamage;
        }
        
    }
}

