using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyShooting : MonoBehaviour // MACACO/SHOOTING
{

    public GameObject coco;
    public Transform cocoPos;
    private GameObject player;

    private float timer;
    private float distance;

    public EnemyShooting macaco;
    public Collider2D colider;
    public Rigidbody2D rig;
    private Animator anim;
    
    public bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        macaco = GetComponent<EnemyShooting>();
        colider = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(player != null)
        {
            Flip();
            distance = UnityEngine.Vector2.Distance(transform.position, player.transform.position);
        }

        if(distance < 6.7)
        {
            timer += Time.deltaTime;
            if(timer > 0.01)
            {
                isHit = false;
                timer = 0;
                anim.SetTrigger("Attack");
            }
        }else
        {
            anim.Play("Macaco_idle");
        }

    }

    void ShootEvent()
    {
        Instantiate(coco, cocoPos.position, Quaternion.identity);
    }

    void Flip()
    {
        if (player == null) return;

        Vector3 scale = transform.localScale;

        // Vira para esquerda se o player estiver à esquerda
        if (player.transform.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1f;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
}
