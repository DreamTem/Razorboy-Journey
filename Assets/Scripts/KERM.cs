using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KERM : MonoBehaviour
{
    [Range(0, 30)]
    public float speed;
  
    //kermit moving
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //kermit destroy
        if (collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
