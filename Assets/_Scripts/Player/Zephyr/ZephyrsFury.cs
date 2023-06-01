using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZephyrsFury : MonoBehaviour
{
    [SerializeField] GameObject hitSpark;
    [SerializeField] GameObject hitSpark2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            // Deal Damage
            enemy.TakeDamage(PlayerManager.instance.player_SO.attackDamage + PlayerManager.instance.player_SO.zephyrsFuryDamage);

            // Hit Spark
            Instantiate(hitSpark, collision.transform.position, collision.transform.rotation);
            Instantiate(hitSpark2, collision.transform.position, transform.rotation, collision.transform);
        }
    }
}
