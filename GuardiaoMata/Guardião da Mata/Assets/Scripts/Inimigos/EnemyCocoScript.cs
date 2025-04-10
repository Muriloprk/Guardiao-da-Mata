using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCocoScript : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rig;

    public float force;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            UnityEngine.Vector2 direction = player.transform.position - transform.position;
            rig.velocity = new UnityEngine.Vector2(direction.x, direction.y).normalized * force;
        }
        else
        {
            Destroy(gameObject);
        }
    
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            collision.gameObject.GetComponent<LifeSystem>().vida -= 1;

            player.GetComponent<Player>().anim.Play("Hit");
            player.GetComponent<Player>().anim.SetBool("isHit", true);

            UnityEngine.Vector2 knockDirection = (player.transform.position.x < transform.position.x) ? new UnityEngine.Vector2(-1, 0.6f) : new UnityEngine.Vector2(1, 0.6f);
            player.GetComponent<Player>().ApplyKnockback(knockDirection);
            Destroy(gameObject);
        }
    }
}
