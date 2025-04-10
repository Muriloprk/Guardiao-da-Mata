using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            distance = UnityEngine.Vector2.Distance(transform.position, player.transform.position);
        }

        if(distance < 6.7)
        {
            timer += Time.deltaTime;
            if(timer > 2)
            {
                timer = 0;
                anim.Play("Macaco_hit");
                shoot();
            }
        }else
        {
            anim.Play("Macaco_idle");
        }

    }

    void shoot()
    {
        Instantiate(coco, cocoPos.position, Quaternion.identity);
    }
}
