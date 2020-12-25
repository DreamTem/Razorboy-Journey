using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCar : MonoBehaviour
{
    [Range(0, 30)]
    public float speed;
    //miniCar moving
    void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //desrtoying
        if(collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
