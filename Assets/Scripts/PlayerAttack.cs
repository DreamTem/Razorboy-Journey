using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    
    public static bool block,press,boxing,canDamage;
    public static float stamina = 5, maxStam = 5;
    public float timeBtwAttack;
    [Range(1, 2.5f)]
    public  float startTimeBtwAttack;
    public Animator anim;
    public Slider stamin;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    [Range(0.65f, 4)]
    public static int NPCHP=3;
    public float attackRNGX,attackser,attackRNGY;
    public AudioSource playerAudio;
    public AudioClip arm;
    // Update is called once per frame
    void Start()
    {
        canDamage = true;
        boxing = false;
        press = false;
        block=true;
        attackser=0;
        stamina = 5;
        maxStam = 5;
    }
    void Update()
    {
        //block pressed
        if(press) 
        {
            anim.SetBool("Block", true);
            EnemyAttack.Damage = 0;
            EnemyAttack.LeggDamage = 0;
        }
        //block unpressed
        else
        {
            anim.SetBool("Block", false);
            EnemyAttack.Damage = 1;
            EnemyAttack.LeggDamage = 2;
        }
        stamin.value=stamina/5;
        //arming, when disarmed
        if (!canDamage && !press)
        {
            Invoke("canDamageSwitcher", 0.45f);
        }
        //stamina filling
        if (stamina < maxStam&&MoveFox.shft==false&&MoveFox.staminaFill==MoveFox.maxStaminaFill)
        {
            stamina += Time.deltaTime;
        }

        if (stamina < 0)
        {
            stamina = 0;
        }
        //block in
        if (Input.GetKeyDown(KeyCode.JoystickButton3) && !MoveFox.running && canDamage && !MoveFox.ELEV && !MoveFox.dialogStop && !CarSummon.carActive && !MoveFox.falling && !MoveFox.jmp && !Crouching.isCrouching)
        {
            press = true;
        }
        //block out
        if (Input.GetKeyUp(KeyCode.JoystickButton3) && press)
        {
            press = false;
        }
        if(stamina>0.9f)
        {
            if (timeBtwAttack <= 0)
            {
                //attack
                if (Input.GetKeyDown(KeyCode.JoystickButton1) && !MoveFox.jmp && !MoveFox.running && !Crouching.isCrouching && !MoveFox.dialogStop && !CarSummon.carActive && !press) 
                {
                    if (canDamage == true)
                    {
                        //collider, what checking enemies
                        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRNGX, attackRNGY), 0, whatIsEnemies);
                        if (attackser == 0)
                        {
                            anim.SetTrigger("Att");
                            if (SlowMo.slowEnabled == false)
                            {
                                stamina -= 1.5f;
                            }
                            Invoke("att1", 0.25f);
                        }
                        if (attackser == 1)
                        {
                            anim.SetTrigger("att2");
                            if (SlowMo.slowEnabled == false)
                            {
                                stamina -= 1.5f;
                            }
                            Invoke("attt2", 0.25f);
                        }
                        for (int i = 0; i < enemiesToDamage.Length; i++)
                        {
                            playerAudio.PlayOneShot(arm, 0.75f);
                            if (EnemyAttack.isBlocking == false && boxing == false)
                            {
                                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(1);
                                
                            }
                            
                        }
                        timeBtwAttack = startTimeBtwAttack;
                    }
                }
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;

            }
            
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRNGX,attackRNGY,1));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //laser colliding
        if (collision.gameObject.CompareTag("Shot"))
        {
            Destroy(collision.gameObject);            
            GameControlScript.health -= 2;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        boxing = false;
    }
    //attack animation 1
    public void att1()
    {
        attackser = 1;
    }
    //attack animation 2
    public void attt2()
    {
        attackser = 0;
    }
    //damage availabling
    void canDamageSwitcher()
    {
        canDamage = true;
    }
}



