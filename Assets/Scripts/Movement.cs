using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
   
    public float playerspeed = 10.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Transform cam;

    Vector3 velocity;
    bool isGrounded;

    //Dialogue with NPC
    [HideInInspector] public bool npcInRange;
    public Character npc;
    public KeyCode talkKey;
    public float interactRange = 2f;
    public DialogueSystem dialogueSystem;

    public bool isTalking;

    // Start is called before the first frame update
    void Start()
    {
        transform.position.Set(500,5,1000);
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            Debug.Log("isGrounded");
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

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

        Vector3 move = new Vector3(horizontal, 0, vertical).normalized; //.normalized makes sure we don't move faster diagonally

        if (move.magnitude >= 0.1f)
        {
            float TargetAngle = (Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg) + cam.eulerAngles.y ;
            //Atan2 returns the angle the x-axis and a vector starting at zero and terminating at x,y
            //The result will be in radians, thus mathf.rad2deg converts radians to degrees.
            transform.rotation = Quaternion.Euler(0f, TargetAngle, 0f);

            Vector3 Dir = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward;
            controller.Move(Dir.normalized * Time.deltaTime * playerspeed);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Jump");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

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

        var col = Physics.OverlapSphere(transform.position, interactRange, LayerMask.GetMask("NPC"));
        print("Colliders in Range: " + col.Length);
        if (col.Length > 0)
        {
            npc = col[0].gameObject.GetComponent<Character>();
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
