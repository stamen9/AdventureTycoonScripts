using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [System.Serializable]
    public class TutorialNode
    {
        public string TutorialName;
        public Dialogue TutorialDialogue = null;
        [HideInInspector] public bool hasRan = false;
    }

    [SerializeField]public List<TutorialNode> TutorialCollection = new List<TutorialNode>();

    public void FindAndPlayDialogue(string name)
    {
        foreach(TutorialNode node in TutorialCollection)
        {
            if(node.TutorialName == name)
            {
                //breaking should be fine as names should be unique*
                if (node.hasRan)
                    break;
                DialogueSystem.Instance.PlayDialogue(node.TutorialDialogue);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FindAndPlayDialogue("TestDialog");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
