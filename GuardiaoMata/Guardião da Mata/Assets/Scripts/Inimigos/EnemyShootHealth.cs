using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootHealth : MonoBehaviour
{

    public float health;
    private Animator anim;
    public EnemyShooting macaco;
    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Morrer();
    }

    void Morrer()
    {
        if(health<=0)
        {
            
            macaco.colider.enabled = false; //Desativa o colisor do corpo
            macaco.rig.constraints = RigidbodyConstraints2D.FreezePosition; //Trava a posição em que morreu
            macaco.enabled = false; // Desativa o script do macaco
            anim.Play("macaco_die");
            Invoke("Destruir",0.5f);
        }
    }

    void Destruir()
    {
        Destroy(gameObject);
    }
}
