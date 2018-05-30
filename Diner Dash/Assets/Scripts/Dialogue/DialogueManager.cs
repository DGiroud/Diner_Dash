using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Image displayPic;
    

    public Animator animator;

    public Queue<string> sentences;
    private Queue<Sprite> dPs;



    void Start()
    {
        sentences = new Queue<string>();
        dPs = new Queue<Sprite>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0.0f;
        GameObject.Find("GameController").GetComponent<TutorialGameController>().canvas.transform.GetComponent<TutorialPauseMenu>().gameIsPaused = true;

        animator.SetBool("IsOpen", true);       

        nameText.text = dialogue.name;        

        sentences.Clear();

        dPs.Clear();        

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (Sprite image in dialogue.displayPics)
        {
            dPs.Enqueue(image);
        }

        DisplayNextSentence();
        DisplayNextSprite();
    }

    void SetImage()
    {
        
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSprite()
    {
        if (sentences.Count == 0)
        {            
            return;
        }

        Sprite sprite = dPs.Dequeue();
        displayPic.sprite = sprite;
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Time.timeScale = 1.0f;
        GameObject.Find("GameController").GetComponent<TutorialGameController>().canvas.transform.GetComponent<TutorialPauseMenu>().gameIsPaused = false;
        animator.SetBool("IsOpen", false);
    }
}