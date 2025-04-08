using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{

    private Player player;

    public int vida;
    public int vidaMaxima;

    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        LifeLogic();
        DeadState();
    }

    void LifeLogic()
    {
        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vida)
            {
                coracao[i].sprite = cheio;
            }else{
                coracao[i].sprite = vazio;
            }

            if (i < vidaMaxima)
            {
                coracao[i].enabled = true;
            }else{
                coracao[i].enabled = false;
            }
        }
    }

    void DeadState()
    {
        if (vida <= 0)
        {

            player.bodyCollider.enabled = false; //Desativa o colisor do corpo
            player.rig.constraints = RigidbodyConstraints2D.FreezePosition; //Trava a posição em que morreu
            player.enabled = false; // Desativa o script do Player
            player.anim.Play("Die");

            // Aplica o knockback final na morte
            Vector2 deathKnockback = new Vector2(player.transform.localScale.x * -1, 0.5f);
            player.ApplyKnockback(deathKnockback);

            player.rig.velocity = Vector2.zero; // Para qualquer movimento

            Destroy(gameObject, 1.2f);

            GameController.instance.ShowGameOver();
        }
    }

    
}
