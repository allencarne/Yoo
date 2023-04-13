using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingGust : MonoBehaviour
{
    [SerializeField] GameObject sweepingGustPrefab;
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
        //Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            // Deal Damage
            enemy.TakeDamage(1);

            // Hit Spark
            Instantiate(zephyrHitSpark, collision.transform.position, collision.transform.rotation);
            Instantiate(sweepingGustHitSpark, collision.transform.position, transform.rotation, collision.transform);

            // Destroy this
            Destroy(gameObject);

            // Pull in the opposide direction the gameObject is facing
            var direction = rb.velocity.normalized;
            collision.attachedRigidbody.velocity = -direction * 10;

            var sg2 = Instantiate(sweepingGustPrefab, transform.position, collision.transform.rotation);
            sg2.GetComponent<Rigidbody2D>().velocity = -direction * 10;
            Destroy(sg2, .3f);
        }
    }
}
