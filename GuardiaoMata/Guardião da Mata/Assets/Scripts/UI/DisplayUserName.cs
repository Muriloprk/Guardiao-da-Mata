using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using Firebase.Auth; 

public class DisplayUserName : MonoBehaviour
{
    public TMP_Text userNameText; 

    void Start()
    {
        if (userNameText != null)
        {
            userNameText.text = ""; 
            userNameText.gameObject.SetActive(false); 
        }

        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;

        if (user != null)
        {
            if (!string.IsNullOrEmpty(user.DisplayName))
            {
                if (userNameText != null)
                {
                    userNameText.text = "Bem-vindo(a), " + user.DisplayName + "!";
                    userNameText.gameObject.SetActive(true); 
                }
            }
            else
            {
                if (userNameText != null)
                {
                    userNameText.text = "Bem-vindo(a), " + user.Email + "!"; 
                    userNameText.gameObject.SetActive(true);
                }
                Debug.LogWarning("DisplayName do usuário está vazio. Exibindo E-mail como alternativa.");
            }
        }
        else
        {
            Debug.LogWarning("Nenhum usuário autenticado encontrado. O texto do nome de usuário não será exibido.");
        }
    }
}