using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTelegraph : MonoBehaviour
{
    [SerializeField] GameObject hitBox;

    public void AE_EndOfAnimation()
    {
        Instantiate(hitBox, transform.position, transform.rotation);
    }
}
