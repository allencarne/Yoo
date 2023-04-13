using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] GameObject enemyHitSpark;
    [SerializeField] GameObject enemySpecificHitSpark;

    public static event System.Action OnPlayerParry;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Rigidbody2D playerRB = collision.GetComponent<Rigidbody2D>();

        if (collision.tag == "Player" && player)
        {
            // Deal Damage
            player.TakeDamage(1);

            // Hit Spark
            Instantiate(enemyHitSpark, collision.transform.position, collision.transform.rotation);
            Instantiate(enemySpecificHitSpark, collision.transform.position, transform.rotation, collision.transform);
        }

        if (collision.tag == "Parry")
        {
            OnPlayerParry?.Invoke();
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
