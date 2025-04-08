using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public string lvlName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o player tocou no next level
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(lvlName);
        }

    }

}
