using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainUI : MonoBehaviour
{

    TextMeshProUGUI interactionDebug;
    TextMeshProUGUI interactiveText;
    TextMeshProUGUI interactiveName;
    GameObject crosshair;
    GameObject interactiveBar;

    GameObject TaskBar;
    TextMeshProUGUI TaskText;

    GameObject TaskBarTake;
    GameObject TaskBarTaken;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        interactionDebug = GameObject.Find("interaction").GetComponent<TextMeshProUGUI>();
        interactiveText = GameObject.Find("interactiveText").GetComponent<TextMeshProUGUI>();
        interactiveName = GameObject.Find("interactiveName").GetComponent<TextMeshProUGUI>();
        crosshair = GameObject.Find("crosshair");
        interactiveBar = GameObject.Find("interactiveBar");
        TaskBar = GameObject.Find("TaskBar");
        TaskText = GameObject.Find("TaskText").GetComponent<TextMeshProUGUI>();
        TaskBar.SetActive(false);
        TaskBarTake = TaskBar.transform.Find("TaskBarTake").gameObject;
        TaskBarTaken = TaskBar.transform.Find("TaskBarTaken").gameObject;
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInteractText();

        
        if (player.cameraRotation) { crosshair.SetActive(true); }
        else { crosshair.SetActive(false); }

        if (player.playerInteracting) { interactiveBar.SetActive(true); }
        else { interactiveBar.SetActive(false); }

        npcInteractiveText();
    }
    public void nextConvo()
    {
        Npc info = player.getAimedObject()
            .transform.parent.GetComponent<Npc>();
        if (!info.hasTask)
        {
            TaskBar.SetActive(false);
            if (info.conversation.Count - 1 > info.interactiveIndex)
            {
                info.interactiveIndex++;
            }
            else
            {
                info.interactiveIndex = 0;
                player.cameraRotation = true;
                player.playerInteracting = false;
            }
        }
        else
        {
            if (info.conversation.Count > info.interactiveIndex)
            {
                info.interactiveIndex++;
            }

            else
            {
                info.interactiveIndex = 0;
                player.cameraRotation = true;
                player.playerInteracting = false;
            }
        }
        
        
    }
    public void TaskBarShow()
    {
        TaskBar.SetActive(true);
        Task task = player.getAimedObject().transform.parent.GetComponent<Task>();

        TaskText.text = task.Name;
        if (!task.taken)
        {
            TaskBarTake.SetActive(true);
            TaskBarTaken.SetActive(false);
        }
        else
        {
            TaskBarTake.SetActive(false);
            TaskBarTaken.SetActive(true);
        }
    }
    public void TaskBarHide()
    {
        TaskBar.SetActive(false);
    }
    public void TakeTask()
    {
        if (player.taskCapacity <= player.tasks.Count) { return; }
        player.tasks.Add(player.getAimedObject().transform.parent.GetComponent<Task>());


        player.getAimedObject().transform.parent.GetComponent<Task>().taken = true;
        player.cameraRotation = true;
        player.playerInteracting = false;
        
        return;
    }
    void playerInteractText()
    {

        interactionDebug.text = "";
        GameObject target = GameObject.Find("Player").GetComponent<Player>().getAimedObject();
        if (target == null )
        {
            return;
        }
        if(target.transform.parent.GetComponent<Npc>() == null)
        {
            return;
        }
        if (!target.transform.parent.GetComponent<Npc>().playerInRange)
        {
            return;
        }
        if (!target.transform.parent.GetComponent<Npc>().interactable)
        {
            return; 
        }
        if (target.tag != "npc")
        {
            return;
        }

        //pokial iteraguje
        if (GameObject.Find("Player").GetComponent<Player>().playerInteracting) { return; }


        interactionDebug.text = "Press E to interact";
    }
    void npcInteractiveText()
    {
        

        if (!player.playerInteracting) { return; }
        
        if (player.getAimedObject() == null)
        {
            player.cameraRotation = true;
            player.playerInteracting = false;
            return;

        }
        if (!player.getAimedObject().transform.parent.GetComponent<Npc>().playerInRange)
        {
            player.cameraRotation = true;
            player.playerInteracting = false;
            return;
        }
        interactiveText.text = player.getAimedObject()
            .transform.parent.GetComponent<Npc>().interctiveText;
        interactiveName.text = player.getAimedObject().transform.parent.name;
    }
}
