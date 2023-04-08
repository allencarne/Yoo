using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingGust : MonoBehaviour
{
    [SerializeField] GameObject zephyrHitSpark;
    [SerializeField] GameObject sweepingGustHitSpark;

    // Damage
    // sweeping Gust Pull Force

    private void Update()
    {
        //Debug.Log();
    }

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
            Instantiate(sweepingGustHitSpark, collision.transform.position, transform.rotation, collision.transform);

            // Pull
            Vector2 direction = (enemy.transform.position - transform.rotation.eulerAngles).normalized;

            enemyRB.velocity = direction * 10;
        }
    }
}
