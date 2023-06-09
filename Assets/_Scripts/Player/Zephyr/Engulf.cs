using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engulf : MonoBehaviour
{
    [SerializeField] GameObject zephyrHitSpark;
    [SerializeField] GameObject windSlashHitSpark;
    [SerializeField] Transform center;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            // Deal Damage
            enemy.TakeDamage(PlayerManager.instance.player_SO.attackDamage + PlayerManager.instance.player_SO.zephyrsFuryDamage);

            // Hit Spark
            Instantiate(zephyrHitSpark, collision.transform.position, collision.transform.rotation);
            Instantiate(windSlashHitSpark, collision.transform.position, transform.rotation, collision.transform);

            // KnockBack
            Vector2 direction = (center.position - enemy.transform.position).normalized;
            enemyRB.velocity = direction * PlayerManager.instance.player_SO.zephyrsFuryPullForce;
        }
    }
}
