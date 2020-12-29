using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    public Queue<string> Sentences;
    public string Speakers;

    public GameObject panel;

    public bool isInDialogue;

    public TMPro.TMP_Text DialogueName;
    public TMPro.TMP_Text DialogueText;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Sentences = new Queue<string>();

            panel.SetActive(false);

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlayDialogue(Dialogue dialogue)
    {

        Sentences.Clear();

        isInDialogue = true;
        //Debug
        foreach (string sen in dialogue.Lines)
        {
                Sentences.Enqueue(sen);
        }

        DialogueName.text = dialogue.Talker;
        //Skin animations for now
        //DialogueAnimator.SetBool("IsOpen", true);
        panel.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (Sentences.Count == 0)
        {
            StopDialogue();
            return;
        }

        string sentence = Sentences.Dequeue();

        //DialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";

        foreach (char character in sentence.ToCharArray())
        {
            DialogueText.text += character;
            yield return 0.01f;
        }
    }

    public void StopDialogue()
    {
        //Skin animations for now
        //DialogueAnimator.SetBool("IsOpen", false);
        isInDialogue = false;
        panel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
