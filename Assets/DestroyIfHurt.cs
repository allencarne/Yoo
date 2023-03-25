using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfHurt : MonoBehaviour
{
    private void OnEnable()
    {
        Enemy.OnEnemyHurt += DestroyIfHurtt;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyHurt -= DestroyIfHurtt;
    }

    public void DestroyIfHurtt()
    {
        Destroy(gameObject);
    }
}
