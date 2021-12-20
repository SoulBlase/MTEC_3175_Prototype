using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    //public Theme theme;

    public GameObject canvas; 
    public Character characterLeft;
    public Character characterRight;
    public GameObject leftCharacterImageObject;
    public GameObject rightCharacaterImageObject;

    public GameObject leftCharacterNameBox;
    public GameObject rightCharacterNameBox;
    public TextMeshProUGUI dialoguetextBox;

    public List<Character> allCharacters = new List<Character>();
    public TextAsset textSheet;

    [Range(0.01f,0.25f)]public float timeBetweenChars = 0.1f;
    public const int dialogueCharacterLimit = 191;

    private WaitForSeconds charDelay;

    public bool isTyping;


    void Start()
    {
        charDelay = new WaitForSeconds(timeBetweenChars);
        ToggleCanvas(false);

        if (textSheet != null)
        {
            ParseDoc(textSheet);
        }      
    }

    private void ToggleCanvas(bool status)
    {
        canvas.SetActive(status);
    }


    private void SetCharacter(int index, Character character)
    {

        if (index < 0)
        {
            characterLeft = character;
            //leftCharacterImageObject.SetActive(true);
            //leftCharacterImageObject.GetComponentInChildren<RawImage>().texture = character.characterImage;

            leftCharacterNameBox.SetActive(true);
            leftCharacterNameBox.GetComponent<Image>().color = characterLeft.characterColor;
            leftCharacterNameBox.GetComponentInChildren<TextMeshProUGUI>().text = character.characterName;
        }

        if (index > 0)
        {
            characterRight = character;
            //rightCharacaterImageObject.SetActive(true);
            //rightCharacaterImageObject.GetComponentInChildren<RawImage>().texture = character.characterImage;

            rightCharacterNameBox.SetActive(true);
            rightCharacterNameBox.GetComponent<Image>().color = characterRight.characterColor;
            rightCharacterNameBox.GetComponentInChildren<TextMeshProUGUI>().text = character.characterName;
        }


    }

    public void StartDialogue(Character cLeft, Character cRight = null)
    {
        ToggleCanvas(true);

        if (cLeft != null)
        {
            SetCharacter(-1, cLeft);
        }
        else
        {
            leftCharacterImageObject.SetActive(false);
            leftCharacterNameBox.SetActive(false);
        }

        if (cRight != null)
        {
            SetCharacter(1, cRight);
        }
        else
        {
            rightCharacaterImageObject.SetActive(false);
            rightCharacterNameBox.SetActive(false);
        }


        var dialogue = GetTexts(cLeft.GetDialogueOption());

        StartCoroutine(GoThroughDialogue(dialogue));

    }

    public void EndDialogue()
    {
        ToggleCanvas(false);

    }

    private string[] GetTexts(char[] c)
    {
        if (c.Length > dialogueCharacterLimit)
        {
            string[] s = new string[Mathf.RoundToInt(c.Length%dialogueCharacterLimit)];
            print(s.Length);
            for (int i = 0; i < c.Length; i++)
            {
                int x = Mathf.FloorToInt(i % dialogueCharacterLimit);
                print(x);
                s[x] += c[i];

            }

            return s;

        }
        else
        {
            string[] s = new string[1];
            s[0] = c.ArrayToString();
            return s;
        }
    }

    public void SkipTyping()
    {
        charDelay = new WaitForSeconds(0);

    }

    private IEnumerator GoThroughDialogue(string [] texts)
    {
        WaitUntil typeDone = new WaitUntil(() => !isTyping);
        for (int i = 0; i < texts.Length; i++)
        {
            StartCoroutine(DisplayText(texts[i]));
            yield return typeDone;
        }

    }

    private IEnumerator DisplayText(string text)
    {
        isTyping = true;
        var chars = text.ToCharArray();
        string t = "";

        int limit = Mathf.Min(chars.Length, dialogueCharacterLimit);

        for (int i = 0; i < limit; i++)
        {
            t += chars[i];
            dialoguetextBox.text = t;
            yield return charDelay;
        }

        isTyping = false;
        charDelay = new WaitForSeconds(timeBetweenChars);
    }

    private void ParseDoc(TextAsset doc)
    {

        string text = doc.text;
        string[] lines = text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] cells = line.Split('\t');
            foreach (var character in allCharacters)
            {
                if (cells[0] == character.characterID)
                {
                    for (int j = 1; j < cells.Length; j++)
                    {
                        character.dialogueTexts.Add(cells[j]);
                    }
                }

            }

        }
    }
}
