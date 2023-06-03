using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainUI : MonoBehaviour
{

    TextMeshProUGUI debugspeed;
    TextMeshProUGUI interactionDebug;
    // Start is called before the first frame update
    void Start()
    {
        debugspeed = GameObject.Find("debug speed").GetComponent<TextMeshProUGUI>();
        interactionDebug = GameObject.Find("interaction").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        debugspeed.text = "speed: " + GameObject.Find("Player").GetComponent<Rigidbody>().velocity.ToString();
        interactText();

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
