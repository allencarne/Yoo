using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] GameObject enemyHitSpark;
    [SerializeField] GameObject enemySpecificHitSpark;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Rigidbody2D playerRB = collision.GetComponent<Rigidbody2D>();

        if (player != null)
        {
            // Deal Damage
            player.TakeDamage(1);

            // Hit Spark
            Instantiate(enemyHitSpark, collision.transform.position, collision.transform.rotation);
            Instantiate(enemySpecificHitSpark, collision.transform.position, transform.rotation, collision.transform);
        }
    }
}
