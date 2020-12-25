using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Rigidbody2D rb2d;

    //Explode method
    public void Explode()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.gravityScale = 2f;
        rb2d.velocity = (new Vector2(rb2d.velocity.x, 30));
        transform.Rotate(Vector2.right*10*Time.deltaTime);
        Invoke("Destroying",2f);
    }
    //destroy soon after exploding
    public void Destroying()
    {
         Destroy(gameObject);
    }
}
