using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    [Header("Vida")]
    public float health;

    [Header("Animações")]
    public string takeHitAnim; // Pode deixar vazio se não tiver
    public string deathAnim;
    private Animator anim;

    [Header("Referências")]
    public Rigidbody2D rig;
    public Collider2D col;
    public MonoBehaviour behaviorScript; // ex: Enemy.cs ou EnemyShooting.cs
    private EnemyShooting macaco;

    private bool isDead = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        macaco = GetComponent<EnemyShooting>();
        behaviorScript.GetComponent<EnemyShooting>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;

        if (!string.IsNullOrEmpty(takeHitAnim))
        {
            anim.Play(takeHitAnim);
            behaviorScript.GetComponent<EnemyShooting>().isHit = true;
        }
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (col != null) col.enabled = false;
        if (rig != null) rig.constraints = RigidbodyConstraints2D.FreezeAll;
        if (behaviorScript != null) behaviorScript.enabled = false;

        anim.Play(deathAnim);
        Invoke("DestroyEnemy", 0.5f);
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            TakeDamage(1f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.GetComponent<LifeSystem>().vida--;

                // Direção do knockback
                Vector2 knockDir = (player.transform.position.x < transform.position.x) ? new Vector2(-1, 0.6f) : new Vector2(1, 0.6f);
                player.ApplyKnockback(knockDir);

                player.anim.Play("Hit");
                player.anim.SetBool("isHit", true);
            }
        }
    }
}