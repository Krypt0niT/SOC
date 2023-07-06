using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class transportTask : MonoBehaviour
{
    GameObject place0;
    GameObject place1;


    public int numberOfCrates0;
    public int numberOfCrates1;
    public int MaxNumberOfCrates;
    public Task task;
    GameObject boxesGameobject;
    public GameObject playerInRange = null;

    TextMeshPro info0;
    TextMeshPro info1;
    

    void Start()
    {
        place0 = this.GetComponent<transportInfo>().plate0;
        place1 = this.GetComponent<transportInfo>().plate1;
        MaxNumberOfCrates = Random.Range(2,5);
        numberOfCrates0 = MaxNumberOfCrates;
        numberOfCrates1 = 0;

        info0 = this.gameObject.transform.Find("transportLocation0").transform.Find("Info").gameObject.GetComponent<TextMeshPro>();
        info1 = this.gameObject.transform.Find("transportLocation1").transform.Find("Info").gameObject.GetComponent<TextMeshPro>();

        info0.enabled = true;
        info1.enabled = true;

        GameObject gObj = this.GetComponent<transportInfo>().boxesGameobject;

        gObj.GetComponent<Boxes>().type = 0;

        Instantiate(gObj).transform.parent 
            = this.gameObject.transform.Find("transportLocation0");

        gObj.GetComponent<Boxes>().type = 1;

        Instantiate(gObj).transform.parent
            = this.gameObject.transform.Find("transportLocation1");






    }

    // Update is called once per frame
    void Update()
    {
        DestroyControll();

        task.other = numberOfCrates1.ToString() + " / " + MaxNumberOfCrates.ToString();
        //finnishControll();
        textUpdate();
        

    }
    public void finnishControll()
    {
        if (numberOfCrates1 == MaxNumberOfCrates)
        {
            for (int i = 0; i < GameObject.FindObjectOfType<Player>().tasks.Count; i++)
            {
                if (GameObject.FindObjectOfType<Player>().getAimedObject().transform.parent.GetComponent<Task>().Name ==
                    GameObject.FindGameObjectsWithTag("task")[i].transform.Find("PlayerTaskName").gameObject.GetComponent<TextMeshProUGUI>().text)
                {
                    Destroy(GameObject.FindGameObjectsWithTag("task")[i]);
                }
            }
            task.completed = true;
            Destroy(this.gameObject.transform.Find("transportLocation0").GetComponentInChildren<Boxes>().gameObject);
            Destroy(this.gameObject.transform.Find("transportLocation1").GetComponentInChildren<Boxes>().gameObject);
            task.gameObject.GetComponent<Npc>().interactable = false;
            GameObject.FindObjectOfType<Player>().hideInteractiveBar();

            info0.enabled = false;
            info1.enabled = false;

            Destroy(this);
        }
    }
    void DestroyControll()
    {
        if (task != null) return;
        
        Destroy(this.gameObject.transform.Find("transportLocation0").GetComponentInChildren<Boxes>().gameObject);   
        Destroy(this.gameObject.transform.Find("transportLocation1").GetComponentInChildren<Boxes>().gameObject);

        info0.enabled = false;
        info1.enabled = false;
        Destroy(this);
    }
    void textUpdate()
    {
        info0.gameObject.transform.LookAt(GameObject.FindObjectOfType<Player>().gameObject.transform);
        info0.gameObject.transform.rotation = Quaternion.Euler(
            0,
            info0.gameObject.transform.rotation.eulerAngles.y + 180,
            info0.gameObject.transform.rotation.eulerAngles.z
            );

        //------------

        info1.gameObject.transform.LookAt(GameObject.FindObjectOfType<Player>().gameObject.transform);
        info1.gameObject.transform.rotation = Quaternion.Euler(
            0,
            info1.gameObject.transform.rotation.eulerAngles.y + 180,
            info1.gameObject.transform.rotation.eulerAngles.z
            );

        // info
        if(numberOfCrates0 > 0)
        {
            info0.text = "Press " + GameObject.FindObjectOfType<Settings>().interactKey + " to pick up.";
            info0.text += "<br>Boxes left: " + numberOfCrates0;
        }
        else
        {
            info0.text = "";
        }
        

        if (GameObject.FindObjectOfType<Player>().carringObj != null)
        {
            info1.text = "Press " + GameObject.FindObjectOfType<Settings>().interactKey + " to put down.";
            info1.text += "<br>" + numberOfCrates1 + "/" + MaxNumberOfCrates;
        }
        else if (numberOfCrates1 == MaxNumberOfCrates)
        {
            info1.text = "COMPLETED!";
        }
        else
        {
            info1.text = "";
        }
        
    }
}
