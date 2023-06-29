using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(this.gameObject.transform.parent.GetComponent<Npc>() != null)
            {
                transform.parent.GetComponent<Npc>().playerInRange = true;

            }
            if (this.gameObject.transform.parent.parent.GetComponent<transportTask>() != null)
            {
                this.gameObject.transform.parent.parent.GetComponent<transportTask>().playerInRange = this.transform.parent.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.gameObject.transform.parent.GetComponent<Npc>() != null)
            {
                transform.parent.GetComponent<Npc>().playerInRange = false;
            }
            if (this.gameObject.transform.parent.parent.GetComponent<transportTask>() != null)
            {
                this.gameObject.transform.parent.parent.GetComponent<transportTask>().playerInRange = null;
            }
        }
    }
}
