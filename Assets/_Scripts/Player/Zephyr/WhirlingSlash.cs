using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingSlash : MonoBehaviour
{
        [SerializeField] GameObject contactHitSpark;
    [SerializeField] GameObject collisionHitSpark;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();

        var player = PlayerManager.instance.playerInstance;

        if (enemy && player)
        {
            // Deal Damage
            enemy.TakeDamage(PlayerManager.instance.player_SO.attackDamage + PlayerManager.instance.player_SO.whirlingSlashDamage);

            // Hit Spark
            Instantiate(contactHitSpark, collision.transform.position, collision.transform.rotation);
            Instantiate(collisionHitSpark, collision.transform.position, transform.rotation, collision.transform);

            // KnockBack
            enemy.KnockBack(enemy.transform.position, transform.position, enemyRB, PlayerManager.instance.player_SO.whirlingSlashKnockBackForce);
        }
    }
}
