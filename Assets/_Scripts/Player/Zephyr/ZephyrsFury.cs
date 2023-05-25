using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZephyrsFury : MonoBehaviour
{
    [SerializeField] GameObject hitSpark;
    [SerializeField] GameObject hitSpark2;
    //[SerializeField] Transform center;
    float coolDownTime = .5f;

    bool canAttack = true;

    private void Update()
    {
        Destroy(gameObject, 999f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {


            // KnockBack
            //Vector2 direction = (PlayerManager.instance.playerInstance.transform.position - enemy.transform.position).normalized;
            //enemyRB.velocity = direction * 5;

            StartCoroutine(cooldown(enemy));
        }
    }

    IEnumerator cooldown(Enemy enemy)
    {
        if (canAttack)
        {
            canAttack = !canAttack;

            // Deal Damage
            enemy.TakeDamage(PlayerManager.instance.player_SO.attackDamage + PlayerManager.instance.player_SO.engulfDamage);

            // Hit Spark
            Instantiate(hitSpark, enemy.transform.position, enemy.transform.rotation);
            Instantiate(hitSpark2, enemy.transform.position, transform.rotation, enemy.transform);

            yield return new WaitForSeconds(coolDownTime);

            canAttack = !canAttack;
        }
    }
}
