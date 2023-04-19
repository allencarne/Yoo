using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Item coin;

    public static event System.Action OnCoinCollected;

    public void Collect()
    {
        // Event for sounds and other things
        //OnCoinCollected?.Invoke();

        // If inventory space is not full, collect the coin
        bool wasCollected = Inventory.instance.Add(coin);

        // Destroy coin if it was collected
        if (wasCollected)
        {
            Destroy(gameObject);
        }
    }
}
