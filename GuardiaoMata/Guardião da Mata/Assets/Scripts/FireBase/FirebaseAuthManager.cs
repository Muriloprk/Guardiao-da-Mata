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
    public TMP_InputField nomeInput;
    public TMP_Text mensagemErro;
    private FirebaseAuth auth;

    void Start()
    {
        mensagemErro.gameObject.SetActive(false);

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == Firebase.DependencyStatus.Available)
            {
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

    public void Cadastrar()
    {
        mensagemErro.gameObject.SetActive(false);
        string email = emailInput.text.Trim();
        string senha = senhaInput.text.Trim();
        string nome = nomeInput.text.Trim();  // Obtém o nome/apelido

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(nome))
        {
            mensagemErro.text = "Preencha todos os campos.";
            mensagemErro.gameObject.SetActive(true);
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, senha).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                FirebaseUser newUser = task.Result.User;  // Obtém o usuário criado

                // Atualiza o perfil com o Nome/Apelido
                UserProfile profile = new UserProfile { DisplayName = nome };

                newUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(profileTask =>
                {
                    if (profileTask.IsCompleted && !profileTask.IsFaulted)
                    {
                        // Sucesso, vai para a próxima cena
                        SceneManager.LoadScene("TelaJogarSair");
                    }
                    else
                    {
                        mensagemErro.text = "Erro ao salvar nome/apelido.";
                        mensagemErro.gameObject.SetActive(true);
                    }
                });
            }
            else
            {
                FirebaseException fbEx = task.Exception?.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)(fbEx != null ? fbEx.ErrorCode : -1);

                switch (errorCode)
                {
                    case AuthError.EmailAlreadyInUse:
                        mensagemErro.text = "Este e-mail já está cadastrado.";
                        break;
                    case AuthError.InvalidEmail:
                        mensagemErro.text = "E-mail inválido.";
                        break;
                    case AuthError.WeakPassword:
                        mensagemErro.text = "Senha fraca. Use 6 caracteres ou mais.";
                        break;
                    default:
                        mensagemErro.text = $"Erro ao cadastrar: {(fbEx != null ? fbEx.Message : "Tente novamente.")}";
                        break;
                }

                mensagemErro.gameObject.SetActive(true);
            }
        });
    }


    public void Login()
    {
        mensagemErro.gameObject.SetActive(false);
        string email = emailInput.text.Trim();
        string senha = senhaInput.text.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            mensagemErro.text = "Preencha todos os campos.";
            mensagemErro.gameObject.SetActive(true);
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, senha).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                SceneManager.LoadScene("TelaJogarSair");
            }
            else
            {
                FirebaseException fbEx = task.Exception?.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)(fbEx != null ? fbEx.ErrorCode : -1);

                switch (errorCode)
                {
                    case AuthError.InvalidEmail:
                        mensagemErro.text = "E-mail inválido.";
                        break;
                    case AuthError.UserNotFound:
                    case AuthError.WrongPassword:
                        mensagemErro.text = "E-mail ou senha incorretos.";
                        break;
                    default:
                        mensagemErro.text = "Erro: " + (fbEx?.Message ?? "Erro desconhecido.");
                        break;
                }

                mensagemErro.gameObject.SetActive(true);
            }
        });
    }

}
