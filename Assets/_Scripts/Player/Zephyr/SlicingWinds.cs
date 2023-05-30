using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicingWinds : MonoBehaviour
{
    [SerializeField] GameObject zephyrHitSpark;
    [SerializeField] GameObject windSlashHitSpark;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            // Debuff
            enemy.Vulnerability();

            // Deal Damage
            enemy.TakeDamage(PlayerManager.instance.player_SO.attackDamage + PlayerManager.instance.player_SO.slicingWindsDamage);

            // Hit Spark
            Instantiate(zephyrHitSpark, collision.transform.position, collision.transform.rotation);
            Instantiate(windSlashHitSpark, collision.transform.position, transform.rotation, collision.transform);

            // KnockBack
            enemy.KnockBack(enemy.transform.position, transform.position, enemyRB, PlayerManager.instance.player_SO.slicingWindsKnockBackForce);

        }
    }
}
