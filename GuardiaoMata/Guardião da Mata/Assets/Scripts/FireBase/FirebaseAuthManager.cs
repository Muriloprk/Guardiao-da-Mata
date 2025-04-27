using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;

public class FirebaseAuthManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField senhaInput;
    public TMP_Text mensagemErro;
    private FirebaseAuth auth;

    void Start()
    {
        mensagemErro.gameObject.SetActive(false);

        // Verifica e corrige as dependências do Firebase e inicializa o app manualmente
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == Firebase.DependencyStatus.Available)
            {
                // Cria o app com as opções manuais
                FirebaseApp app = FirebaseApp.Create(new AppOptions
                {
                    ApiKey = "AIzaSyCBVVPAxnSsAh2UoIycdtRe0KI169BPTYk",
                    ProjectId = "guardiaodamata-acdf6",
                    AppId = "314681092117"
                });
                
                auth = FirebaseAuth.GetAuth(app);
                Debug.Log("✅ Firebase inicializado manualmente para desktop!");
            }
            else
            {
                Debug.LogError("❌ Erro ao inicializar Firebase: " + task.Result);
            }
        });
    }

    // Método chamado para cadastrar um novo usuário
    public void Cadastrar()
    {
        Debug.Log("Cadastrar chamado!");
        string email = emailInput.text;
        string senha = senhaInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, senha).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                SceneManager.LoadScene("TelaJogarSair");
            }
            else
            {
                mensagemErro.text = "Erro ao cadastrar: " + (task.Exception?.GetBaseException().Message ?? "Erro desconhecido");
                mensagemErro.gameObject.SetActive(true);
            }
        });
    }

    // Método chamado para realizar o login de um usuário
    public void Login()
    {
        string email = emailInput.text;
        string senha = senhaInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, senha).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                SceneManager.LoadScene("TelaJogarSair");
            }
            else
            {
                mensagemErro.text = "Erro ao logar: " + (task.Exception?.GetBaseException().Message ?? "Erro desconhecido");
                mensagemErro.gameObject.SetActive(true);
            }
        });
    }
}
