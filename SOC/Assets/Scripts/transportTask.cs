using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transportTask : MonoBehaviour
{
    GameObject place0;
    GameObject place1;


    public int numberOfCrates0;
    public int numberOfCrates1;
    public int MaxNumberOfCrates;
    int cratesDone = 0;
    public Task task;
    GameObject boxesGameobject;

    

    void Start()
    {
        place0 = this.GetComponent<transportInfo>().plate0;
        place1 = this.GetComponent<transportInfo>().plate1;
        MaxNumberOfCrates = Random.Range(2,5);
        print(MaxNumberOfCrates);
        numberOfCrates0 = MaxNumberOfCrates;
        numberOfCrates1 = 0;

        GameObject gObj = this.GetComponent<transportInfo>().boxesGameobject;

        Instantiate(gObj).transform.parent 
            = this.gameObject.transform.Find("transportLocation0");
        gObj.GetComponent<Boxes>().type = 0;


        Instantiate(gObj).transform.parent
            = this.gameObject.transform.Find("transportLocation1");
        gObj.GetComponent<Boxes>().type = 1;






    }

    // Update is called once per frame
    void Update()
    {
        task.other = cratesDone.ToString() + " / " + MaxNumberOfCrates.ToString();
        finnishControll();
    }
    void finnishControll()
    {
        if (numberOfCrates1 == MaxNumberOfCrates)
        {
            task.completed = true;
            Destroy(this.gameObject.transform.Find("transportLocation0").GetComponentInChildren<Boxes>().gameObject);
            Destroy(this.gameObject.transform.Find("transportLocation1").GetComponentInChildren<Boxes>().gameObject);
            task.gameObject.GetComponent<Npc>().interactable = false;


            Destroy(this);
        }
    }
}
