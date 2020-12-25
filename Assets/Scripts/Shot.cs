using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet,bullet2;
    public LayerMask whatIsPlayer;
    public float attackRangeX;
    public float attackRangeY;
    private float timeBtwAttack;
    [Range(0.5f, 3.4f)]
    public float startTimeBtwAttack;
    public Animator anim;
    public AudioSource LaserEnemy;
    public AudioClip LaserAttack;
    public static bool canDamage,AIStop;


    void Start()
    {
        AIStop = false;
        canDamage = true;
    }
    void FixedUpdate()
    {
        //move switch
        if (AIStop == true)
        {
            Invoke("AImove", 0.3f);
        }

        //arming when disarmed
        if (canDamage == false)
        {
            Invoke("canDamageSwitcher", 1);
        }
        //shooting
        if (timeBtwAttack <= 0)
        {
            if(canDamage == true)
            {
                //player check collider
                Collider2D[] PlayerToDamage = Physics2D.OverlapBoxAll(firePoint.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsPlayer);

                //shooting
                for (int i = 0; i < PlayerToDamage.Length; i++)
                {
                    AIStop = true;
                    Shoot();
                }
                timeBtwAttack = startTimeBtwAttack;
            }

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;

        }
        
    }
    //shooting method
    void Shoot()
    {
        LaserEnemy.PlayOneShot(LaserAttack, 0.6f);
        anim.SetTrigger("Shot");

        //right bullet shoot
        if (ShootPatrol.movingRight==true)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }

        //left bullet shoot
        else if (ShootPatrol.movingLeft == true)
        {
            Instantiate(bullet2, firePoint.position, firePoint.rotation);
        }
    }

    //showing shooting zone in the scene
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(firePoint.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
    //arming when disarmed method
    void canDamageSwitcher()
    {       
         canDamage = true;
    }
    //move switch method
    void AImove()
    {
        AIStop = false;
    }
}
