using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryStrike : MonoBehaviour
{
    [SerializeField] GameObject zephyrHitSpark;
    [SerializeField] GameObject parryStrikeHitSpark;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            // Deal Damage
            enemy.TakeDamage(1);

            // Hit Spark
            Instantiate(zephyrHitSpark, collision.transform.position, collision.transform.rotation);
            Instantiate(parryStrikeHitSpark, collision.transform.position, transform.rotation, collision.transform);

            // KnockBack
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            enemyRB.velocity = direction * 4;
        }
    }
}
