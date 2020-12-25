using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR : MonoBehaviour
{
    [Range(0, 30)]
    public float speed;

    void FixedUpdate()
    {
        //moving
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //car destroying
        if(collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
