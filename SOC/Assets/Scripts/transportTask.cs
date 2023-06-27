using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transportTask : MonoBehaviour
{
    GameObject place0;
    GameObject place1;


    public int numberOfCrates;
    public int MaxNumberOfCrates;
    int cratesDone = 0;
    public Task task;
    GameObject boxesGameobject;

    

    void Start()
    {
        place0 = this.GetComponent<transportInfo>().plate0;
        place1 = this.GetComponent<transportInfo>().plate1;
        MaxNumberOfCrates = Random.Range(1,5);
        numberOfCrates = MaxNumberOfCrates;

        Instantiate(this.GetComponent<transportInfo>().boxesGameobject).transform.parent 
            = this.gameObject.transform.Find("transportLocation0");




    }

    // Update is called once per frame
    void Update()
    {
        task.other = cratesDone.ToString() + " / " + MaxNumberOfCrates.ToString();
    }
    
}
