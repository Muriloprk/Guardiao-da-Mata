using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    private Animator anim;
    public Enemy tatu;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
        {
            
            tatu.colider.enabled = false; //Desativa o colisor do corpo
            tatu.rig.constraints = RigidbodyConstraints2D.FreezePosition; //Trava a posição em que morreu
            tatu.enabled = false; // Desativa o script do tatu
            anim.Play("Tatu_die");
            Invoke("Destruir",0.5f);
        }
    }

    void Destruir()
    {
        Destroy(gameObject);
    }
}
