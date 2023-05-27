using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZephyrsFury : MonoBehaviour
{
    //[SerializeField] GameObject hitSpark;
    //[SerializeField] GameObject hitSpark2;
    //[SerializeField] Transform center;

    private void Update()
    {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            // Deal Damage
            //enemy.TakeDamage(PlayerManager.instance.player_SO.attackDamage + PlayerManager.instance.player_SO.engulfDamage);

            // Hit Spark
            //Instantiate(hitSpark, collision.transform.position, collision.transform.rotation);
            //Instantiate(hitSpark2, collision.transform.position, transform.rotation, collision.transform);

            // KnockBack
            enemy.KnockBack(PlayerManager.instance.playerInstance.transform.position, enemy.transform.position, enemyRB, 10);
        }
    }
}
