using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainUI : MonoBehaviour
{

    TextMeshProUGUI debugspeed;
    TextMeshProUGUI interactionDebug;
    GameObject crosshair;
    GameObject interactiveBar;
    // Start is called before the first frame update
    void Start()
    {
        debugspeed = GameObject.Find("debug speed").GetComponent<TextMeshProUGUI>();
        interactionDebug = GameObject.Find("interaction").GetComponent<TextMeshProUGUI>();
        crosshair = GameObject.Find("crosshair");
        interactiveBar = GameObject.Find("interactiveBar");
    }

    // Update is called once per frame
    void Update()
    {
        debugspeed.text = "speed: " + GameObject.Find("Player").GetComponent<Rigidbody>().velocity.ToString();
        interactText();


        if (GameObject.Find("Player").GetComponent<Player>().cameraRotation) { crosshair.SetActive(true); }
        else { crosshair.SetActive(false); }

        if (GameObject.Find("Player").GetComponent<Player>().playerInteracting) { interactiveBar.SetActive(true); }
        else { interactiveBar.SetActive(false); }
    }
    void interactText()
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
        interactionDebug.text = "Press E to interact";
    }
}
