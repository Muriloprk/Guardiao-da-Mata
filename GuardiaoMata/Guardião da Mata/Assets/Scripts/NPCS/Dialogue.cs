using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    
    [System.Serializable]
    public class DialogueLine
    {
        public Sprite speakerIcon;  // Ícone do falante
        public string speakerName;   // "Saci" ou "Poatan"
        [TextArea(3, 5)] public string text; // Texto da fala
    }

    public DialogueLine[] dialogueLines; // Todas as falas em ordem
    public LayerMask playerLayer;
    public float radius;

    private DialogueControl dc;
    private bool onRadius;
    private GameObject player;

    void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && onRadius && dc.CurrentState == DialogueEnums.State.Ready)
        {
            dc.StartDialogue(dialogueLines);
        }

        if(player != null)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        onRadius = (hit != null);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
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