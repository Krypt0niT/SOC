using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    TextMeshPro symbol;



    private void Start()
    {
        symbol = this.gameObject.transform.Find("symbol").GetComponent<TextMeshPro>();
    }



    private void Update()
    {
        taskFinder();
        convoChange();
        symbolUpdate();
    }
    void symbolUpdate()
    {
        symbol.gameObject.transform.LookAt(GameObject.FindObjectOfType<Player>().gameObject.transform);
        symbol.gameObject.transform.rotation = Quaternion.Euler(
            0,
            symbol.gameObject.transform.rotation.eulerAngles.y + 180,
            symbol.gameObject.transform.rotation.eulerAngles.z
            );
        if (hasTask)
        {
            symbol.text = "?";
            return;
        }
        if (interactable)
        {
            symbol.text = "!";
            return;
        }
        symbol.text = "";
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
                    if (GameObject.FindObjectOfType<Player>().playerInteracting)
                    {
                        GameObject.FindAnyObjectByType<MainUI>().TaskBarShow();
                    }
                }
                else
                {
                    GameObject.FindAnyObjectByType<MainUI>().TaskBarHide();
                    interctiveText = conversation[interactiveIndex];
                }
            }
        }
        if (!GameObject.FindAnyObjectByType<Player>().playerInteracting)
        {
            GameObject.FindAnyObjectByType<MainUI>().TaskBarHide();
        }
    }
    
}
