using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginner : Player
{
    protected override void Start()
    {
        base.Start();

        sword.SetActive(false);
    }
}
