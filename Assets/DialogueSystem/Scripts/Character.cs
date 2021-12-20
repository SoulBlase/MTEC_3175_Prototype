using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isNPC;
    public bool playerInRange; 
    public string characterID;
    public string characterName;
    public Texture characterImage;
    public Color characterColor;
    public AudioSource characterAudioSource;
    [HideInInspector]public Movement player;
   

    public List<string> dialogueTexts = new List<string>(); 


    public GameObject talkPrompt;

    void Awake()
    {
        if (isNPC)
        {
            player = GameObject.Find("Player").GetComponent<Movement>();
            //gameObject.GetComponent<SpriteRenderer>().color = characterColor;
        }
    }


    private void Update()
    {
        if (isNPC)
        {
            talkPrompt.SetActive(playerInRange);
        }
        
    }

    public char[] GetDialogueOption(int index)
    {
        //char[] s = dialogueTexts[index].ToCharArray();

        return dialogueTexts[index].ToCharArray();
    }

    public char[] GetDialogueOption()
    {
        string t = dialogueTexts[Random.Range(0, dialogueTexts.Count)];

        return t.ToCharArray();
    }


}
