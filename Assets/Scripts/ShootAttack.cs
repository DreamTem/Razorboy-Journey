using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : MonoBehaviour
{
    private float timeBtwAttack;
    [Range(1, 2.5f)]
    public  float startTimeBtwAttack;

    public Animator anim;
    public Transform attackPos;
    public LayerMask shot;
    public float attackRangeX,attackser;
    public float attackRangeY;
    public static int damage = 1;
    public AudioSource playerAudio;
    public AudioClip arm;

    // Start is called before the first frame update
    void Start()
    {
        attackser=0;
    }

    // Update is called once per frame
    void Update()
    {
        //time between attack change
        if (PlayerAttack.stamina == 5)
        {
            startTimeBtwAttack = 1;
        }
        if (PlayerAttack.stamina == 4)
        {
            startTimeBtwAttack = 1.2f;
        }
        if (PlayerAttack.stamina == 3)
        {
            startTimeBtwAttack = 1.4f;
        }
        if (PlayerAttack.stamina == 2)
        {
            startTimeBtwAttack = 1.8f;
        }


        if (PlayerAttack.stamina>0.9f)
        {
            if (timeBtwAttack <= 0)
            {
                if (Input.GetKeyDown(KeyCode.JoystickButton1) && !MoveFox.running && !MoveFox.jmp && !MoveFox.dialogStop && !CarSummon.carActive && !PlayerAttack.press)
                {
                    //shooting enemy check collider
                    Collider2D[] enemisToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, shot);

                    //attack
                    for (int i = 0; i < enemisToDamage.Length; i++)
                    {
                        
                        playerAudio.PlayOneShot(arm, 0.75f);
                        enemisToDamage[i].GetComponent<ENHL>().TkeDamage(damage);
                    }
                    timeBtwAttack = startTimeBtwAttack;
                }
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;

            }
        }
    }
    //overlap box showing
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
    //attack animation 1
    public void att1(){
        attackser=1;
    }
    //attack animation 2
    public void attt2(){
        attackser=0;
    }
}
