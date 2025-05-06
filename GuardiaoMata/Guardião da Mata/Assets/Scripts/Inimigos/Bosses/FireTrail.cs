using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireTrail : MonoBehaviour
{

    public float lifetime;
    public AudioSource audioDano;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.GetComponent<LifeSystem>().vida--;
                audioDano.Play();

                // Direção do knockback
                Vector2 knockDir = (player.transform.position.x < transform.position.x) ? new Vector2(-1, 0.6f) : new Vector2(1, 0.6f);
                player.ApplyKnockback(knockDir);

                player.anim.Play("Hit");
                player.anim.SetBool("isHit", true);
            }
        }
    }
}
