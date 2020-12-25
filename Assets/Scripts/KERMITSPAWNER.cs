using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KERMITSPAWNER : MonoBehaviour
{
  [Range(1,120)]
    public float stDelay;

    private float spawnLimitY = 15;
    private float spawnPosX = -500f;
    public GameObject FlyingCar;
    void Start()
    {
        Invoke("Spawn", stDelay);
    }
    //kermit spawning
    void Spawn()
    {
        Invoke("Spawn", stDelay);
        Vector2 spawnPos = new Vector3(spawnPosX, spawnLimitY, 6);
        Instantiate(FlyingCar, spawnPos, FlyingCar.transform.rotation);
    }
}
