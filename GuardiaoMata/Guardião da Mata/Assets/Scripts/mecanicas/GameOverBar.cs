using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBar : MonoBehaviour

{
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
