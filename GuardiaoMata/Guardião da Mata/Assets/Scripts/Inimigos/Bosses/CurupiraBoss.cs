using System.Collections;
using UnityEngine;

public class CurupiraBoss : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false;

    [Header("Configurações de Movimento")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float wallCheckDistance = 0.2f;
    private float currentSpeed;
    private bool isFacingRight = true;

    [Header("Componentes")]
    public Rigidbody2D rb;
    public Collider2D col;
    public Animator anim;
    public Transform wallCheck;

    [Header("Fase 2 - Fogo")]
    public float phase2Threshold = 50f; // 50% de vida
    public GameObject fireTrailPrefab;
    public float fireSpawnInterval = 0.1f;
    private float nextFireTime;
    private bool isInPhase2 = false;

    [Header("Animações")]
    public string walkAnim = "Curupira_Walk";
    public string runFireAnim = "Curupira_RunFire";
    public string hitAnim = "Curupira_Hit";
    public string deathAnim = "Curupira_Death";


    public LayerMask groundLayer;
    public MonoBehaviour behaviorScript;
    public AudioSource audioDano;
    public AudioSource audioMorte;
    public AudioSource audioFase2;

    void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = walkSpeed;
        anim.Play(walkAnim);
    }

    void Update()
    {
        if (isDead) return;

        CheckPhaseTransition();
        HandleMovement();
        HandleFireTrail();

    }

    void CheckPhaseTransition()
    {
        if (!isInPhase2 && currentHealth <= phase2Threshold)
        {
            EnterPhase2();
        }
    }

    void EnterPhase2()
    {
        isInPhase2 = true;
        currentSpeed = runSpeed;
        audioFase2.Play();
        anim.Play(runFireAnim);
    }

    void HandleMovement()
    {
        if (isDead) return;

        

        // Direção do movimento
        float direction = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * currentSpeed, rb.velocity.y);

        // Verifica colisão com parede
        Vector2 wallDirection = isFacingRight ? Vector2.right : Vector2.left;
        bool hitWall = Physics2D.Raycast(wallCheck.position, wallDirection, wallCheckDistance, groundLayer);

        Debug.DrawRay(wallCheck.position, wallDirection * wallCheckDistance, Color.red);

        if (hitWall)
        {
            Debug.Log("Bateu na parede, girando");
            Flip();
        }
    }

    void HandleFireTrail()
    {
        if (isInPhase2 && Time.time >= nextFireTime)
        {
            Vector3 firePosition = new Vector3(transform.position.x, transform.position.y - 0.858f, transform.position.z); // Ajuste no eixo Y
            Instantiate(fireTrailPrefab, firePosition, Quaternion.identity);
            nextFireTime = Time.time + fireSpawnInterval;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        // Inverte apenas o sprite visual
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (isFacingRight ? 1 : -1);
        transform.localScale = scale;

        anim.Play(isInPhase2 ? runFireAnim : walkAnim);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        StartCoroutine(PlayHitAnimation());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (col != null) col.enabled = false;
        if (rb != null) rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (behaviorScript != null) behaviorScript.enabled = false;

        anim.Play(deathAnim);
        audioMorte.Play();
        Destroy(gameObject, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        // Recebe dano de ataques do jogador
        if (other.CompareTag("Attack"))
        {
            TakeDamage(1f); // Valor ajustável conforme seu sistema
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead || !collision.gameObject.CompareTag("Player")) return;

        Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.GetComponent<LifeSystem>().vida--;
                audioDano.Play();

                // Direção do knockback
                Vector2 knockDir = (player.transform.position.x < transform.position.x) ? new Vector2(-1, 0.6f) : new Vector2(1, 0.6f);
                player.ApplyKnockback(knockDir);

                player.anim.Play("Hit");
                player.anim.SetBool("isHit", true);
            }
    }

    IEnumerator PlayHitAnimation()
    {
        anim.Play(hitAnim);
        yield return new WaitForSeconds(0.2f);
        if (!isDead) // Evita voltar para andar se ele morreu após o hit
        {
            anim.Play(isInPhase2 ? runFireAnim : walkAnim);
        }
    }

}