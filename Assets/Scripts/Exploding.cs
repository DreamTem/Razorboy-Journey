using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", .5f);
    }
    //explosion end
    void Explode(){
        Destroy(gameObject);
    }
}
