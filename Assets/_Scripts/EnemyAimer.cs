using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimer : MonoBehaviour
{
    public float offset;
    public Transform firePoint;
    Transform target;
    public static bool pauseDirection = false;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!pauseDirection)
        {
            Rotate();
        }
    }

    public void Rotate()
    {
        transform.up = transform.position - target.transform.position;
    }
}
