using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    [HideInInspector] public bool npcInRange;
    public Character npc;
    public KeyCode talkKey;
    public float interactRange = 2f;
    public DialogueSystem dialogueSystem;

    public bool isTalking;


    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        if (isTalking)
        {
            if (Input.GetKeyDown(talkKey))
            {
                if (dialogueSystem.isTyping)
                {
                    dialogueSystem.SkipTyping();
                }
                else
                {
                    dialogueSystem.EndDialogue();
                    isTalking = false;
                }
                
            }

            return;
        }

        transform.Translate(xMove * moveSpeed * Time.deltaTime, yMove * Time.deltaTime * moveSpeed, 0);

        GetNPCInRange();

        if (npcInRange)
        {
            if (Input.GetKeyDown(talkKey))
            {
                dialogueSystem.StartDialogue(npc);
                isTalking = true;
            }
        }     
    }

    private void GetNPCInRange()
    {

        var col = Physics2D.OverlapCircle(transform.position, interactRange, LayerMask.GetMask("NPC"));
        if (col != null)
        {
            npc = col.gameObject.GetComponent<Character>();
            npc.playerInRange = true;
            npcInRange = true;
        }
        else
        {
            if (npc != null)
            {
                npc.playerInRange = false;
                npc = null;
            }
            npcInRange = false;
        }
    }

}
