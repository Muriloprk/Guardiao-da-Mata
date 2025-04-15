using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image dialogueBox;
    [SerializeField] private Image profileImage;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header ("Components")]
    public GameObject dialogueObj;

    [Header("Settings")]
    public float typingSpeed = 0.05f;

    private Dialogue.DialogueLine[] currentLines;
    private int currentIndex;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private DialogueEnums.State state = DialogueEnums.State.Ready;
    
    public DialogueEnums.State CurrentState => state;

    public bool isDialogueActive = false;
    

    public void StartDialogue(Dialogue.DialogueLine[] lines)
    {
        if (state != DialogueEnums.State.Ready|| lines == null || lines.Length == 0) return;

        currentLines = lines;
        currentIndex = 0;
        state = DialogueEnums.State.Active;
        isDialogueActive = true;
        dialogueObj.SetActive(true);
        DisplayNextLine();
    }

    public bool IsDialogueActive()
    {
        return dialogueObj.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (state == DialogueEnums.State.Active && dialogueObj.activeSelf)
            {
                if (isTyping)
                {
                    SkipTyping();
                }
                else
                {
                    DisplayNextLine();
                }
            }
        }
    }


    void DisplayNextLine()
    {
        if (currentIndex >= currentLines.Length)
        {
            EndDialogue();
            return;
        }

        Dialogue.DialogueLine line = currentLines[currentIndex];
        profileImage.sprite = line.speakerIcon;
        speakerNameText.text = line.speakerName;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(line.text));
        currentIndex++;
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = currentLines[currentIndex - 1].text;
        isTyping = false;
    }

    

    void EndDialogue()
    {
        isDialogueActive = false;
        state = DialogueEnums.State.Cooldown;
        dialogueObj.SetActive(false);
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(0.2f); // Delay para evitar input acidental
        state = DialogueEnums.State.Ready;
    }
}