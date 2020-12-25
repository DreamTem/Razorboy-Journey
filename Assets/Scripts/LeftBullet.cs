using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBullet : MonoBehaviour
{
    public Rigidbody2D rd;
    // Update is called once per frame
    void FixedUpdate()
    {
        //left bullet moving
        transform.Translate(Vector2.right * 0.75f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //colliding with other objects
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
