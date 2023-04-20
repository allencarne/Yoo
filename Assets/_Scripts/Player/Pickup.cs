using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] KeyCode pickUpKey;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var item = collision.GetComponent<ItemPickup>();
        if (item != null)
        {
            if (Input.GetKey(pickUpKey))
            {
                item.PickUp();
            }
        }
    }
}
