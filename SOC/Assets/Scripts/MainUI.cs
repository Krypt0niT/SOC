using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainUI : MonoBehaviour
{

    TextMeshProUGUI debugspeed;
    TextMeshProUGUI interactionDebug;
    TextMeshProUGUI interactiveText;
    TextMeshProUGUI interactiveName;
    GameObject crosshair;
    GameObject interactiveBar;
    // Start is called before the first frame update
    void Start()
    {
        interactionDebug = GameObject.Find("interaction").GetComponent<TextMeshProUGUI>();
        interactiveText = GameObject.Find("interactiveText").GetComponent<TextMeshProUGUI>();
        interactiveName = GameObject.Find("interactiveName").GetComponent<TextMeshProUGUI>();
        crosshair = GameObject.Find("crosshair");
        interactiveBar = GameObject.Find("interactiveBar");
    }

    // Update is called once per frame
    void Update()
    {
        playerInteractText();


        if (GameObject.Find("Player").GetComponent<Player>().cameraRotation) { crosshair.SetActive(true); }
        else { crosshair.SetActive(false); }

        if (GameObject.Find("Player").GetComponent<Player>().playerInteracting) { interactiveBar.SetActive(true); }
        else { interactiveBar.SetActive(false); }

        npcInteractiveText();
    }
    public void nextConvo()
    {
        Npc info = GameObject.Find("Player").GetComponent<Player>().getAimedObject()
            .transform.parent.GetComponent<Npc>();
        if (info.conversation.Count - 1 > info.interactiveIndex)
        {
            info.interactiveIndex++;
        }
        else
        {
            info.interactiveIndex = 0;
            GameObject.Find("Player").GetComponent<Player>().cameraRotation = true;
            GameObject.Find("Player").GetComponent<Player>().playerInteracting = false;

        }
        
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
        Player player = GameObject.Find("Player").GetComponent<Player>();
        if (!player.playerInteracting) { return; }
        if (player.getAimedObject() == null)
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
