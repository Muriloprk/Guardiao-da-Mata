using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float distance;
    public float wallCheckDistance;

    public Rigidbody2D rig;

    public BoxCollider2D colider;

    bool isRight = true;

    public Transform groundCheck;
    public Transform wallCheck;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        colider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);

        Vector2 direction = isRight ? Vector2.right : Vector2.left;
        RaycastHit2D wall = Physics2D.Raycast(wallCheck.position, direction, wallCheckDistance);

        if(ground.collider == false || wall.collider == true)
        {
            if(isRight == true)
            {
                transform.eulerAngles = new Vector3 (0f, 0f, 0f);
                isRight = false;
                anim.Play("Tatu_run");
            }
            else
            {
                transform.eulerAngles = new Vector3 (0f, 180f, 0f);
                isRight = true;
                anim.Play("Tatu_run");
            }
        }
    }
}
