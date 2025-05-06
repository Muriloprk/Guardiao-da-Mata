using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static float moveInput;
    public float Speed;//velocidade do personagem
    public float JumpForce;//força do pulo
    public bool DoubleJump;//Verifica se possui um double jump
    public bool isGrounded;//Verifica se esta no chão


    public Rigidbody2D rig;//variavel que vai receber o corpo do persongagem

    public Collider2D bodyCollider;
    public Collider2D groundCheckCollider; //colisor do pé pra verificar o pulo
    public Animator anim;

    public float kbForce;
    public float kbCount;
    public float kbTime;

    public bool attackingBool;

    public AudioSource audioAtaque;

    [SerializeField] public BoxCollider2D attack;


    // Start é chamado 1 vez ao iniciar o jogo
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();//instânciando o corpo do boneco(rigidBody2d)
        anim = GetComponent<Animator>();//instânciando o componente animator ligado ao player
        bodyCollider = GetComponent<BoxCollider2D>();
    }

    //Update é chamado 1 vez por frame
    void Update()
    {
        KnockSystem();
        Jump();
        Attack();
    }

    void Move(){

        moveInput = Input.GetAxisRaw("Horizontal");  //pegando tecla que o jogador apertar pela base "horizontal", que para a unity é setas pro lado ou "A" e "D"
        rig.velocity = new Vector2(moveInput * Speed, rig.velocity.y); //executa comando de movimento do jogador, mudando seu eixo X de acordo do a tecla apertada e a velocidade setada

        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector2 (0f,0f);//tranformando o visual do player no angulo 0 e 0 oque mantem ele olhando para a direita.
        }else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector2 (0f,180f); //girando o visual do personagem em 180 graus, fazendo ele olhar para esquerda
        }


        if(isGrounded && kbCount <= 0){
            if(moveInput != 0 && !attackingBool){
                anim.Play("Run");//inicia animação de corrida       
            }else{
                if(!anim.GetBool("isHit") && isGrounded && !attackingBool)
                {
                    anim.Play("Idle");//inicia a animação de parado
                }
            }
        }
    }

    void Jump(){

        if(kbCount > 0) return;

        if (Input.GetButtonDown("Jump"))
        {

            if(isGrounded)
            {
                isGrounded = false;
                rig.velocity = new Vector2(rig.velocity.x, JumpForce);//comando de pulo, modifica o eixo Y do personagem de acordo com a força do pulo
                DoubleJump = true;//habilita pulo duplo
                anim.Play("Jump", -1, 0f);//FORÇA animação de pulo

            }else if(DoubleJump == true)//faz pulo duplo acontecer
            {
                anim.Play("Jump",-1, 0f);//FORÇA animação de pulo
                rig.velocity = new Vector2(rig.velocity.x, JumpForce);//comando de pulo
                DoubleJump = false;//desabilita pulo duplo após pular a segunda vez
            }
        }

    }

    void Attack()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if((Input.GetButtonDown("Fire3") && !anim.GetBool("isHit")) || (Input.GetKeyDown("x") && !anim.GetBool("isHit"))){

            attackingBool = true;
            audioAtaque.Play();

            if(isGrounded)
            {
                if(moveInput != 0)
                {
                    anim.SetBool("AtkGround",  false);
                    anim.SetBool("AtkJump",  false);
                    anim.SetBool("AtkRun",  true);
                    return;
                }
                anim.SetBool("AtkGround",  true);
                anim.SetBool("AtkJump",  false);
                anim.SetBool("AtkRun",  false);
            }
            else
            {
                anim.SetBool("AtkGround",  false);
                anim.SetBool("AtkJump",  true);
                anim.SetBool("AtkRun",  false);
            }

        }

    }

    void EndAttack()
    {
        anim.SetBool("AtkJump",  false);
        anim.SetBool("AtkGround",  false);
        anim.SetBool("AtkRun",  false);
        anim.Play("Idle", -1, 0f);
        attackingBool = false;
    }

    void EndAttackAir()
    {
        anim.SetBool("AtkJump",  false);
        anim.SetBool("AtkGround",  false);
        anim.SetBool("AtkRun",  false);
        anim.Play("Jump", -1, 0f);
        attackingBool = false;
    }

    void EndAttackRun()
    {
        anim.SetBool("AtkJump",  false);
        anim.SetBool("AtkGround",  false);
        anim.SetBool("AtkRun",  false);
        anim.Play("Run", -1, 0f);
        attackingBool = false;
    }



    void KnockSystem()
    {
        if (kbCount > 0)
        {
            kbCount -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("isHit", false);
            Move();
        }
    }

    public void ApplyKnockback(Vector2 knockDirection)
    {
        kbCount = kbTime;  // Define o tempo do knockback
        rig.velocity = Vector2.zero;  // Reseta qualquer movimento anterior
        
        // Ajuste na força do knockback (mude os valores para testar diferentes intensidades)
        Vector2 finalKnockback = new Vector2(knockDirection.x * kbForce, knockDirection.y * (kbForce * 0.8f));
        
        rig.AddForce(finalKnockback, ForceMode2D.Impulse);
    }

     void OnTriggerEnter2D(Collider2D collision)//trigger do colisor do pé
    {

        // Verifica se o Collider de verificação tocou o chão
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true; //Fala que o personagem está no chão
            DoubleJump = true;  // Reseta o pulo duplo
            if(!anim.GetBool("isHit") && !attackingBool)
            {
                anim.Play("Idle");//inicia a animação de parado
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)//trigger do colisor do pé
    {
    
    }
}