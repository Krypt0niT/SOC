using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    TextMeshProUGUI interactionDebug;
    TextMeshProUGUI interactiveText;
    TextMeshProUGUI interactiveName;
    GameObject crosshair;
    public GameObject interactiveBar;

    GameObject TaskBar;
    TextMeshProUGUI TaskText;

    GameObject TaskBarTake;
    GameObject TaskBarTaken;
    GameObject CompleteButton;
    Animation CompleteButtonAnim;

    TextMeshProUGUI MoneyText;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();



        interactionDebug = GameObject.Find("interaction").GetComponent<TextMeshProUGUI>();
        interactiveText = GameObject.Find("interactiveText").GetComponent<TextMeshProUGUI>();
        interactiveName = GameObject.Find("interactiveName").GetComponent<TextMeshProUGUI>();
        crosshair = GameObject.Find("crosshair");

        interactiveBar = GameObject.Find("interactiveBar");
        TaskBar = GameObject.Find("TaskBar");
        TaskText = GameObject.Find("TaskText").GetComponent<TextMeshProUGUI>();
        
        TaskBarTake = GameObject.Find("TaskBarState0").gameObject;
        TaskBarTaken = GameObject.Find("TaskBarState1").gameObject;
        CompleteButton = TaskBarTaken.gameObject.transform.Find("CompleteButton").gameObject;

        MoneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        CompleteButtonAnim = CompleteButton.GetComponent<Animation>();

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
        if (!interactiveBar.activeSelf)
        {
            for (int i = 0; i < GameObject.FindObjectsOfType<Npc>().Length; i++)
            {
                GameObject.FindObjectsOfType<Npc>()[i].interactiveIndex = 0;
            }
        }
        MoneyText.text = GameObject.FindObjectOfType<Manager>().playerMoney.ToString() + "€";

    }
    
    public void nextConvo()
    {
        Npc info = player.getAimedObject()
            .transform.parent.GetComponent<Npc>();
        if (!info.hasTask)
        {
            TaskBarHide();
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
        if (player.getAimedObject() == null) return;
        if (player.getAimedObject().transform.parent == null) return;
        if (player.getAimedObject().transform.parent.GetComponent<Task>() == null) return;
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
            if (player.getAimedObject().transform.parent.GetComponent<Task>().completed)
            {
                CompleteButton.GetComponent<Image>().color = new Color32(3, 188, 19, 255);
                CompleteButtonAnim.Play();
            }
            else
            {
                CompleteButton.GetComponent<Image>().color = new Color32(61, 61, 61, 255);
                CompleteButtonAnim.Stop();
            }

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
        
        TaskBarHide();

        player.getAimedObject().transform.parent.GetComponent<Task>().taken = true;
        player.cameraRotation = true;
        player.playerInteracting = false;
        

        //priradenie tasku pre nahodny objekt ktory tento script este nema

        List <GameObject> transportLocations = new List<GameObject>();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("transportLocation").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("transportLocation")[i].GetComponent<transportTask>() == null)
            {
                transportLocations.Add(GameObject.FindGameObjectsWithTag("transportLocation")[i]);
            }
        }
        GameObject TL = transportLocations[Random.Range(0, transportLocations.Count)];
        TL.AddComponent<transportTask>();
        TL.GetComponent<transportTask>().task = player.tasks[player.tasks.Count - 1];

        TaskBarHide();

        return;
    }
    public void LoseTaskTime()
    {
        List<Task> tasks = GameObject.FindAnyObjectByType<Player>().tasks;
        for (int i = 0; i < tasks.Count; i++)
        {
            
            if (tasks[i].time <= 0)
            {
                for (int j = 0; j < GameObject.FindGameObjectsWithTag("task").Length; j++)
                {
                    if(GameObject.FindGameObjectsWithTag("task")[j]
                        .transform.Find("PlayerTaskName").GetComponent<TextMeshProUGUI>().text == tasks[i].Name)
                    {
                        Destroy(GameObject.FindGameObjectsWithTag("task")[j]); 
                        tasks[i].failed = true;
                        tasks[i].gameObject.GetComponent<Npc>().interactable = false;

                    }


                }

                player.tasks.Remove(tasks[i]);
                
            }
        }
    }
    public void LoseTask()
    {
        for (int i = 0; i < player.tasks.Count; i++)
        {
            if (player.getAimedObject().transform.parent.GetComponent<Task>().Name == 
                GameObject.FindGameObjectsWithTag("task")[i].transform.Find("PlayerTaskName").gameObject.GetComponent<TextMeshProUGUI>().text)
            {
                Destroy(GameObject.FindGameObjectsWithTag("task")[i]);
            }
        }
        player.getAimedObject().transform.parent.GetComponent<Task>().failed = true;
        player.hideInteractiveBar();
        player.tasks.Remove(player.getAimedObject().transform.parent.GetComponent<Task>());
        player.getAimedObject().transform.parent.GetComponent<Task>().gameObject.GetComponent<Npc>().interactable = false;
        

    }
    public void finishTask()
    {
        Task task = player.getAimedObject().transform.parent.GetComponent<Task>();

        if (!task.completed) return;
        for (int i = 0; i < player.tasks.Count; i++)
        {
            if (task.Name == GameObject.FindGameObjectsWithTag("task")[i].transform.Find("PlayerTaskName").gameObject.GetComponent<TextMeshProUGUI>().text)
            {
                Destroy(GameObject.FindGameObjectsWithTag("task")[i]);
            }
        }
        
        player.hideInteractiveBar();
        if (task.paid)
        {
            GameObject.FindObjectOfType<Manager>().playerMoney += task.money;
        }
        player.tasks.Remove(task);
        task.gameObject.GetComponent<Npc>().interactable = false;
        task.gameObject.GetComponent<Npc>().GetComponent<Task>().handedOver = true;


    }
    void playerInteractText()
    {

        interactionDebug.text = "";
        GameObject target = GameObject.Find("Player").GetComponent<Player>().getAimedObject();
        if (target == null )
        {
            return;
        }
        if(target.transform.parent == null) { return; }
        if(target.transform.parent.GetComponent<Npc>() == null) { return; }
        if (!target.transform.parent.GetComponent<Npc>().playerInRange) { return; }
        if (!target.transform.parent.GetComponent<Npc>().interactable) { return; }
        if (target.tag != "npc") { return; }

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
