using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
      public float attackRNGX,attackRNGY;
      public Transform attackPos;
    public LayerMask whatIsDoor, player,enemies,laserEnemies;
    public float speed;
    public static bool ground,rn;
    public Rigidbody2D rigidbody2d;
    public Transform Expl_Spawn_Position;
    [SerializeField]
    private GameObject ExplParticle;
    private Animator anim;
    // Start is called before the first frame update
    private void Start()
    {
        rn = false;
        anim = GetComponent<Animator>();
        ground = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //destroying, when falling for long
        if(rigidbody2d.velocity.y < -12)
        {
            Destroy();
        }
        if (rn)
        {
            anim.SetTrigger("MOVE");
        }
        else
        {
            anim.SetTrigger("MOVENT");
        }
        //destroy after time
        Invoke("Destroy",8);
        //moving
        float horizontal;
        horizontal = Input.GetAxis("Horizontal");
        if(horizontal == 0)
        {
            rn = false;
        }
        //left rotation
        if(horizontal < 0)
        {
            rn = true;
            transform.localScale = new Vector2(-0.75f,0.75f);
        }
        //right rotation
        else if(horizontal > 0)
        {
            rn = true;
            transform.localScale = new Vector2(0.75f,0.75f);
        }

        //moving
        Vector2 position = rigidbody2d.position;
        position.x = position.x + horizontal * speed * Time.fixedDeltaTime;

        rigidbody2d.position = position;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        //enemy damaging
        if(coll.gameObject.CompareTag("Enemy"))
        {
            Destroy(coll.gameObject);
            Instantiate(ExplParticle, Expl_Spawn_Position.position, Expl_Spawn_Position.rotation);
            CarSummon.carActive = false;
            Destroy(transform.parent.gameObject);
        }
        //shooting enemy explode
        if(coll.gameObject.CompareTag("ShootEnemy"))
        {
            Destroy(coll.gameObject);
            Instantiate(ExplParticle, Expl_Spawn_Position.position, Expl_Spawn_Position.rotation);
            CarSummon.carActive = false;
            Destroy(transform.parent.gameObject);
        }
        //player explode
        if(coll.gameObject.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<AttackTake>().Explode();
            Instantiate(ExplParticle, Expl_Spawn_Position.position, Expl_Spawn_Position.rotation);
            CarSummon.carActive = false;
            Destroy(transform.parent.gameObject);
        }
        ground = true;
    }

    private void Destroy(){
        //door explode
        Collider2D[] door = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRNGX, attackRNGY), 0, whatIsDoor);
        for (int i = 0; i < door.Length; i++)
            {
                door[i].GetComponent<Door>().Explode();
            }
        //player explode    
        Collider2D[] playerDetect = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRNGX, attackRNGY), 0, player);
        for(int i = 0; i < playerDetect.Length; i++)
        {
            playerDetect[i].GetComponent<AttackTake>().Explode();
        }
        //enemy range explode
        Collider2D[] enemyDetect = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRNGX, attackRNGY), 0, enemies);
        for(int i = 0; i < enemyDetect.Length; i++)
        {
            Destroy(enemyDetect[i]);
        }
        //shooting enemy range explode
        Collider2D[] shootingEnemyDetect = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRNGX, attackRNGY), 0, laserEnemies);
        for(int i = 0; i < shootingEnemyDetect.Length; i++)
        {
            Destroy(shootingEnemyDetect[i]);
        }
        //creating explosion
        Instantiate(ExplParticle, Expl_Spawn_Position.position, Expl_Spawn_Position.rotation);
        CarSummon.carActive = false;
        Destroy(transform.parent.gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRNGX,attackRNGY,1));
    }
}
