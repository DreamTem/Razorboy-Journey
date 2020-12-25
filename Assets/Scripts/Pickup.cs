using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pickup : MonoBehaviour
{
    public Animator an;
    public AudioSource playerAudio;
    public AudioClip pickup,death;
    public GameObject res;
    public void OnTriggerEnter2D(Collider2D other)
    {
        //checkpointing
        if(other.gameObject.CompareTag("CH"))
        {
            res.gameObject.transform.position=other.gameObject.transform.position;
        }
        //laser die
        if (other.gameObject.CompareTag("Laser"))
        {
            playerAudio.PlayOneShot(death,1);
            GameControlScript.health = 0;
        }    
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        //healing
        if (other.gameObject.CompareTag("Pickup")&&GameControlScript.health<5)
        {
            GameControlScript.health += 1;
            playerAudio.PlayOneShot(pickup, 0.2f);
            Destroy(other.gameObject);
                
        }   
        //aid ignore
        else if(other.gameObject.CompareTag("Pickup")&&GameControlScript.health==5)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),other.GetComponent<Collider2D>());
        }
    }
}

