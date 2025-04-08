using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{

    public LifeSystem life;
    private Player player;
    public EnemyHealth vidaInimigo;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido tem a tag "Player"
       if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();

            if (player != null) 
            {
                life.vida--;

                 if (player.transform.position.x < transform.position.x)
                {
                    player.transform.eulerAngles = new Vector2(0f, 0f); // Virado para a direita
                }
                else
                {
                    player.transform.eulerAngles = new Vector2(0f, 180f); // Virado para a esquerda
                }

                player.anim.Play("Hit");
                player.anim.SetBool("isHit", true);

                Vector2 knockDirection = (player.transform.position.x < transform.position.x) ? new Vector2(-1, 0.6f) : new Vector2(1, 0.6f);
                player.ApplyKnockback(knockDirection);
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Attack"))
        {
            vidaInimigo.health -= 1;
        }
    }


}

