using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("Information")]
    public bool interactable = false;
    public bool hasTask = false;
    public bool playerInRange = false;

    [Header("Conversation")]
    public string interctiveText = "";
    public int interactiveIndex = 0;
    public List<string> conversation = new List<string>();
    





    [SerializeField] Material defaultMaterial;
    [SerializeField] Material interactableMaterial;
    [SerializeField] Material taskMaterial;


    private void Update()
    {
        taskFinder();
        changeColor();
        convoChange();
    }
    void taskFinder()
    {
        if (gameObject.GetComponent<Task>() != null)
        { hasTask = true; }
        else
        { hasTask = false; }
    }
    void convoChange()
    {
        if (conversation.Count != 0)
        {
            if (!hasTask)
            {
                interctiveText = conversation[interactiveIndex];
            }
            else
            {
                if (conversation.Count == interactiveIndex)
                {
                    interctiveText = "";
                    GameObject.FindAnyObjectByType<MainUI>().TaskBarShow();
                }
                else
                {
                    interctiveText = conversation[interactiveIndex];
                }
            }
        }
        if (!GameObject.FindAnyObjectByType<Player>().playerInteracting)
        {
            GameObject.FindAnyObjectByType<MainUI>().HideBarShow();
        }
    }
    void changeColor()
    {
        if (hasTask)
        {
            gameObject.transform.Find("human model").GetComponent<Renderer>().material = taskMaterial;
            return;
        }
        if (interactable)
        {
            gameObject.transform.Find("human model").GetComponent<Renderer>().material = interactableMaterial;
            return;
        }
        gameObject.transform.Find("human model").GetComponent<Renderer>().material = defaultMaterial;

    }
}
