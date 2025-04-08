using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlataforn : MonoBehaviour
{

    public float time;

    private TargetJoint2D target;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    void OnTriggerEnter2D(Collider2D collision)//trigger do colisor do pé
    {
        // Verifica se o Collider de verificação tocou o chão
        if (collision.CompareTag("Player"))
        {
            Invoke("Falling", time);
        }

        if (collision.gameObject.layer == 11)
        {
            Destroy(gameObject);
        }
    }

    void Falling()
    {
        target.enabled = false;
        boxCollider.isTrigger = true;
    }
}
