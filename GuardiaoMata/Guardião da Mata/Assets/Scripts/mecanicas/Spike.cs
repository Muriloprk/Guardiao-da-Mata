using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
         // Verifica se o Collider de verificação tocou o player
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.instance.ShowGameOver();
            Destroy(collision.gameObject);
        }
    }
    // Teste de Commit :)
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o Collider de verificação tocou o player
        if (collision.CompareTag("Player"))
        {
            GameController.instance.ShowGameOver();
            Destroy(collision.gameObject);
        }
    }

}
