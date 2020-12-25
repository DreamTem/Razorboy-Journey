using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveFox : MonoBehaviour
{
    public bool drawDebugRaycasts = true;
    public GameObject tilemap2,cake;
    public SpriteRenderer pickup;
    public GameObject respawn;
    public Rigidbody2D rigidbody2d;
    public float forceJump,jumpAxis;
    public static float speed,ExtraJumps,staminaFill,maxStaminaFill;
    public AudioSource player;
    public AudioClip JUMP;
   public static bool ground,jmp,can,ELEV,rn,running,falling,shft,dialogStop,dialogEnter,raycastBlock,allBlocked;
   public static bool run;
    public Animator animator;
    float jump=0.5f;
    [Header("Ground check")]
    public float groundDistance = .1f;
    public LayerMask groundLayer, platformLayer,enemyLayer,shooting_enemy_layer;

    // Start is called before the first frame update
    void Start()
    {
        cake.SetActive(false);
        pickup.enabled = false;
        tilemap2.SetActive(false);
        allBlocked = false;
        raycastBlock = false;
        dialogEnter = false;
        dialogStop = false;
        ExtraJumps = 0;
        staminaFill = 0;
        shft = false;
        maxStaminaFill = 0.75f;
        falling = false;
        running = false;
        speed = 5f;
        rn = false;
        run = false;
        ELEV = false;
        animator = GetComponent<Animator>();
        can = true;
        jmp = false;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //stun
        if(!PlayerAttack.canDamage)
        {
            rn = false;
            running = false;
        }
        //falling
        if(rigidbody2d.velocity.y < -1f && !ground)
        {
            falling = true;
        }
        //stop falling
        else
        {
            falling = false;
        }
        //joystick jump axis
        jumpAxis = Input.GetAxis("JumpCrouch");
        //stoping mechanics when car, dialoging
        if(CarSummon.carActive)
        {
            rn = false;
            running = false;
        }
        if (dialogStop)
        {
            rn = false;
            running = false;
        }
        //left leg raycast
        RaycastHit2D leftCheck = Raycast(new Vector2(-0.2f, -1.15f), Vector2.down, groundDistance);

        //right leg check
        RaycastHit2D rightCheck = Raycast(new Vector2(0.2f, -1.15f), Vector2.down, groundDistance);

        //left leg enemy raycast
        RaycastHit2D leftEnemyCheck = EnemyRaycast(new Vector2(-0.2f, -1.15f), Vector2.down, groundDistance);

        //right leg enemy check
        RaycastHit2D rightEnemyCheck = EnemyRaycast(new Vector2(0.2f, -1.15f), Vector2.down, groundDistance);

        //left leg enemy raycast
        RaycastHit2D leftShootingEnemyCheck = ShootingEnemyRaycast(new Vector2(-0.2f, -1.15f), Vector2.down, groundDistance);

        //right leg enemy check
        RaycastHit2D rightShootingEnemyCheck = ShootingEnemyRaycast(new Vector2(0.2f, -1.15f), Vector2.down, groundDistance);


        //platform collide check
        RaycastHit2D platCheck = Raycast2(new Vector2(0, -1.15f), Vector2.down, groundDistance);

        //platform landing
        if (platCheck)
        {
            ExtraJumps=0;
            jmp = false;
            running = false;
            rn = false;
            animator.SetTrigger("JMPOUT");
            jmp = false;
            PlayerAttack.block = false;
            ground = true;
            gameObject.transform.parent = platCheck.transform;
        }

        //grounding
        if (leftCheck || rightCheck||leftEnemyCheck||rightEnemyCheck||leftShootingEnemyCheck||rightShootingEnemyCheck)
        {
            ExtraJumps=0;
            jmp = false;
            animator.SetTrigger("JMPOUT");
            PlayerAttack.block = false;
            ground = true;
        }
        //time recover before stamina filling
        if (staminaFill < maxStaminaFill && !shft)
        {
            staminaFill += Time.deltaTime;
        }
        //max stamina recover time setter
        if(staminaFill >= maxStaminaFill)
        {
            staminaFill = maxStaminaFill;
        }
        //jump stopping 
       if (Input.GetButtonUp("Jump"))
        {
            rigidbody2d.velocity = (new Vector2(rigidbody2d.velocity.x, rigidbody2d.velocity.y - 2.25f));
        }

       //falling
        if(falling)
        {
            animator.SetTrigger("OR");
            animator.SetTrigger("JMPOUT");
            animator.SetBool("Fall",true);
        }
        //falling animation deactivating
        if(!falling)
        {
            animator.SetBool("Fall",false);
        }
        //shifting out
        if(PlayerAttack.stamina <= 0||Input.GetKeyUp(KeyCode.JoystickButton2))
        {
            CancelInvoke();
            animator.speed = 1f;
            speed = 5;
            shft = false;
        }
        //fast running activate
       if(Input.GetKeyDown(KeyCode.JoystickButton2))
       {
           if(PlayerAttack.stamina != 0 && rn){
                staminaFill = 0;
               InvokeRepeating("fastRun",0.1f,0.1f);
           }
          
       }      
       //running animation deactivating
        if (!rn)
        {
            animator.SetTrigger("OR");
        }
        //running animation activating
        if (rn && PlayerAttack.canDamage && !CarSummon.carActive && !falling)
        {
            animator.SetTrigger("RUN");
        }      
        //jumping animaton out, when collided
        if(ground)
        {
            animator.SetTrigger("JMPOUT");
        }
        //jumping animation activating
        if (jmp && !falling)
        {
            if (ground == false)
            {
                animator.SetTrigger("Jump");
            }
        }
        //double jump
       if(jmp && ExtraJumps == 0 && (Input.GetButtonDown("Jump")||jumpAxis > 0) && PlayerAttack.stamina > 0.9f && !dialogStop && !RestartingWhileGame.stoped)
       {
            transform.parent = null;
            staminaFill = 0;
            if (SlowMo.slowEnabled == false)
            {
                PlayerAttack.stamina -= 1;
            }
            animator.SetTrigger("JMPOUT");
           animator.SetTrigger("Jump");
           rigidbody2d.velocity = (new Vector2(rigidbody2d.velocity.x, forceJump));
           player.PlayOneShot(JUMP,0.1f);
           ExtraJumps += 1;
       }
        //running
        if (!PlayerAttack.press && !dialogStop && !CarSummon.carActive && PlayerAttack.canDamage && !Crouching.isCrouching && !platCheck && !RestartingWhileGame.stoped && !PlayerAttack.press)
        {
            float horizontal= Input.GetAxis("Horizontal");
            jump = Input.GetAxis("Jump");
            if (horizontal == 0)
            {
                allBlocked = false;
                running = false;
                rn = false;
            }

            //left running
            if (horizontal == -1 && !CheatCode.cheatStop && !dialogStop && !CarSummon.carActive && PlayerAttack.canDamage)
            {
                Invoke("RunStart", 0.1f);
                if (jmp && running)
                {
                    running = false;
                    rn = false;
                }
                PlayerAttack.block = false;
                transform.localScale = new Vector2(-0.762773f, 0.762773f);
                
            }

            //right running
            if (horizontal == 1 && !CheatCode.cheatStop && !dialogStop && !CarSummon.carActive && PlayerAttack.canDamage)
            {
                Invoke("RunStart", 0.1f);
                if (jmp && running)
                {
                    running = false;
                    rn = false;
                }
                PlayerAttack.block = false;
                transform.localScale = new Vector2(0.762773f, 0.762773f);
            }
            //player moving mechanic
            if (!allBlocked)
            {
                Vector2 position = rigidbody2d.position;
                position.x = position.x + horizontal * speed * Time.deltaTime;

                rigidbody2d.position = position;
            }
        }
        //jumping
        if (ground && Input.GetButtonDown("Jump") && !ELEV && PlayerAttack.stamina > 0.9f && !dialogStop && !CarSummon.carActive && PlayerAttack.canDamage && !Crouching.isCrouching && !RestartingWhileGame.stoped && !PlayerAttack.press)
        { 
            staminaFill =0;
            rn = false;
            animator.SetTrigger("OR");
            rigidbody2d.velocity = (new Vector2(rigidbody2d.velocity.x, forceJump));
            player.PlayOneShot(JUMP,0.1f);
            if (!SlowMo.slowEnabled)
            {
                PlayerAttack.stamina -= 0.75f;
            }
            PlayerAttack.block = false;
            Invoke("JMP", 0.02f);
        }      
    }
    public  void OnTriggerExit2D(Collider2D other)
    {
        //secret house deactivate
        if(other.gameObject.CompareTag("Secret"))
        {
            tilemap2.gameObject.SetActive(false);
            pickup.enabled = false;
            cake.SetActive(false);
        }
        dialogEnter = false;
        ELEV = false;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        
        //secret house active
        if(collision.gameObject.CompareTag("Secret"))
        {
            tilemap2.gameObject.SetActive(true);
            pickup.enabled = true;
            cake.SetActive(true);
        }
        //enter to the dialog NPC's collider
        if (collision.gameObject.CompareTag("DIAL")) 
        {
            dialogEnter = true;
        } 
        //enter to the elevator's collider
        if(collision.gameObject.CompareTag("El"))
        {
            ELEV = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //final menu activating
        if(collision.gameObject.CompareTag("BAD"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
    public void OnCollisionStay2D(Collision2D coll)
    { 
        falling=false;
        //running animation, when landed
        if (running && PlayerAttack.canDamage && !Crouching.isCrouching)
        {
            rn = true;
            animator.SetTrigger("RUN");
        }
        jmp = false;
    }
    public void OnCollisionExit2D(Collision2D collision)
    {   
        ground = false;
    }
    //shift fast run method
    public void fastRun(){
        speed = 6.2f;
        if (!SlowMo.slowEnabled)
        {
            PlayerAttack.stamina -= 0.15f;
        }
        animator.speed = 1.75f;
        shft = true;
    }
    //running animation
    public void RunStart(){
        rn = true;
        running = true;
    }
    //jump anim active
    void JMP()
    {
        jmp = true;
        transform.parent = null;
    }
    //some raycasts to detect
    RaycastHit2D EnemyRaycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        return Raycast(offset, rayDirection, length, enemyLayer);
    }
    RaycastHit2D ShootingEnemyRaycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        return Raycast(offset, rayDirection, length, shooting_enemy_layer);
    }
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        return Raycast(offset, rayDirection, length, groundLayer);
    }
    RaycastHit2D Raycast2(Vector2 offset, Vector2 rayDirection, float length)
    {
        return Raycast(offset, rayDirection, length, platformLayer);
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        if (drawDebugRaycasts)
        {
            Color color = hit ? Color.red : Color.green;

            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }

        return hit;
    }
}
