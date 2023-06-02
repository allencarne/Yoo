using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryIfPlayerHurt : MonoBehaviour
{
    private void OnEnable()
    {
        Player.OnPlayerHurt += DestroyIfHurtt;
    }

    private void OnDisable()
    {
        Player.OnPlayerHurt -= DestroyIfHurtt;
    }

    public void DestroyIfHurtt()
    {
        Destroy(gameObject);
    }
}
