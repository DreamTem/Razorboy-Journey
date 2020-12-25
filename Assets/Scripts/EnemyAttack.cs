using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBtwAttack,timeBtwChange;
    [Range(1,3.4f)]
    public float startTimeBtwAttack,startTimeBtwChange;
    public int rand;

    public Transform attackPos;
    private Animator anim;
    public Animator animator;
    public LayerMask whatIsPlayer;
    [Range(0.5f,1)]
    public float attackRange;
   public static int Damage=1;
   public static int LeggDamage=2;
    public AudioSource EnemyAudio;
    public AudioClip arm;
    public static bool isBlocking,AIStop,canDamage;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
        AIStop = false;
        isBlocking = false;
        gameObject.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        //arming when disarmed
        if (canDamage == false)
        {
            Invoke("canDamageSwitcher", 0.7f);
        }
        //move switch
        if (AIStop == true)
        {
            Invoke("AImove", 0.3f);
        }

        //random attack int
        if (timeBtwChange <= 0)
        {
          rand =Random.Range(1, 3);
           timeBtwChange = startTimeBtwChange;
        }
        else
        {
            timeBtwChange -= Time.deltaTime;
        }
        //second attack
        if (timeBtwAttack <= 0&&rand==2)
        {
            if (canDamage == true)
            {
                //player check collider
                Collider2D[] PlayerToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
                for (int i = 0; i < PlayerToDamage.Length; i++)
                {
                    AIStop = true;
                    EnemyAudio.PlayOneShot(arm, 1.5f);
                    anim.SetTrigger("BlockOOut");
                    isBlocking = false;
                    anim.SetTrigger("Legg");
                    PlayerToDamage[i].GetComponent<AttackTake>().TKDMG(LeggDamage);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        //first attack
        if (timeBtwAttack <= 0 && rand==1)
        {
            if (canDamage == true)
            {
                //player check collider
                Collider2D[] PlayerToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
                for (int i = 0; i < PlayerToDamage.Length; i++)
                {
                    AIStop = true;
                    EnemyAudio.PlayOneShot(arm, 1.5f);
                    anim.SetTrigger("BlockOOut");
                    isBlocking = false;
                    anim.SetTrigger("Attack");
                    PlayerToDamage[i].GetComponent<AttackTake>().TakeDamage(Damage);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;

        }
    }   

    //overlapCircle showing
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    //move switch method
    void AImove()
    {
        AIStop = false;
    }
    //arming when disarmed method
    void canDamageSwitcher()
    {
        canDamage = true;
    }
}

