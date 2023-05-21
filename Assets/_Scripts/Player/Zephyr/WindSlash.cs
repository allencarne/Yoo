using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlash : MonoBehaviour
{
    [SerializeField] GameObject zephyrHitSpark;
    [SerializeField] GameObject windSlashHitSpark;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();

        var player = PlayerManager.instance.playerInstance;

        if (enemy && player)
        {
            // Deal Damage
            enemy.TakeDamage(PlayerManager.instance.player_SO.attackDamage + PlayerManager.instance.player_SO.windSlashDamage);

            // Gain Fury
            player.GetComponentInChildren<Zephyr>().GainFury(1);

            // Hit Spark
            Instantiate(zephyrHitSpark, collision.transform.position, collision.transform.rotation);
            Instantiate(windSlashHitSpark, collision.transform.position, transform.rotation, collision.transform);

            // KnockBack
            enemy.KnockBack(enemy.transform.position, transform.position, enemyRB, PlayerManager.instance.player_SO.windSlashKnockBackForce);

            //player.GetComponentInChildren<Zephyr>().KnockBack(enemy.transform.position, transform.position, enemyRB, PlayerManager.instance.player_SO.windSlashKnockBackForce);
            //Vector2 direction = (enemy.transform.position - transform.position).normalized;
            //enemyRB.velocity = direction * PlayerManager.instance.player_SO.windSlashKnockBackForce;
        }
    }
}
