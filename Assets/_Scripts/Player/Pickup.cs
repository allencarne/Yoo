using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    PlayerKeys keys;

    private void Awake()
    {
        keys = GetComponent<PlayerKeys>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var item = collision.GetComponent<ItemPickup>();
        if (item != null)
        {
            if (Input.GetKey(keys.pickUpKey))
            {
                item.PickUp();
            }
        }
    }
}
