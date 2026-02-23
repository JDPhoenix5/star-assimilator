using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    public Animator animator;
    public static Animator talkingCharacter;

    void Start()
    {
        talkingCharacter = animator;
        textComponent.text = string.Empty;
        StartDialogue();

    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            talkingCharacter.SetBool("isTalking", false);
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        talkingCharacter.SetBool("isTalking", true);
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            if (c == ',')
            {
                yield return new WaitForSeconds(textSpeed * 3f);
            }
            if (c == '.')
            {
                yield return new WaitForSeconds(textSpeed * 5f);
            }
            if (c == '?')
            {
                yield return new WaitForSeconds(textSpeed * 5f);
            }
            if (c == '!')
            {
                yield return new WaitForSeconds(textSpeed * 5f);
            }
            if (c == ';')
            {
                Debug.Log("You're using a fucking semicolon. In videogame dialogue. Are you a fucking dork??? Normal people don't even talk like this. You're so fucking performative it pisses me the fuck off. Jesus oh my god FUCK YOU. THIS IS PISSING ME THE FUCK OFF AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
            else
            {
                yield return new WaitForSeconds(textSpeed);
            }
        }
        talkingCharacter.SetBool("isTalking", false);
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Demo");
        }
    }
}
