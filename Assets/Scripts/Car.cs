using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Range(1,8)]
    public float stDelay;

    private float spawnLimitYDown = 10;
    private float spawnLimitYUp = 20;
    private float spawnPosX = -600f;
    public GameObject FlyingCar;
    void Start()
    {
        Invoke("Spawn", stDelay);
    }
    //car spawning
    void Spawn()
    {
        Invoke("Spawn", stDelay);
        Vector2 spawnPos = new Vector3(spawnPosX, Random.Range(spawnLimitYDown, spawnLimitYUp), 6);
        Instantiate(FlyingCar, spawnPos, FlyingCar.transform.rotation);
    }
}
