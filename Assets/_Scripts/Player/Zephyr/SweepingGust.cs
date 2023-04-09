using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingGust : MonoBehaviour
{
    [SerializeField] GameObject zephyrHitSpark;
    [SerializeField] GameObject sweepingGustHitSpark;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, .6f);
    }

    // Damage
    // sweeping Gust Pull Force

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

            // Pull in the opposide direction the gameObject is facing
            //Vector2 direction = (-transform.up).normalized;
            //enemyRB.velocity = -transform.right * 4;

            var direction = rb.velocity.normalized;
            collision.attachedRigidbody.velocity = -direction * 10;
        }
    }
}
