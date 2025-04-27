using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{
    public void IrParaTelaLogin()
    {
        SceneManager.LoadScene("TelaLoginCadastro");
    }

    // Método para ir para a cena "Floresta_pt1"
    public void IrParaTelaFloresta1()
    {
        SceneManager.LoadScene("Floresta_pt1");
    }

    // Método para sair do jogo
    public void Sair()
    {
        Application.Quit();

        // Serve para que ao clicar em sair seja vísivel no Editor do Unity
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
