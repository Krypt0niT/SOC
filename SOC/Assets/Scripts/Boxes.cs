using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    GameObject transportTask;
    GameObject boxes;
    List<GameObject> crates;

    private void Start()
    {
        

        
        transportTask = this.gameObject.transform.parent.transform.parent.gameObject;
        crates = transportTask.GetComponent<transportInfo>().crates;
        boxes = crates[transportTask.GetComponent<transportTask>().numberOfCrates - 1];
        Instantiate(boxes).transform.SetParent(this.gameObject.transform);

    }
    private void Update()
    {
        cratesNumber();
    }
    void cratesNumber()
    {
        string[] split = boxes.name.Split('-');

        
        
        gameObject.transform.localPosition = new Vector3(0,0,0);
        foreach (Transform child in this.gameObject.transform)
        {
            child.transform.localPosition = new Vector3(0,0,0);
        }
        if (transportTask.GetComponent<transportTask>().numberOfCrates == 0)
        {
            return;
        }
        
        if (transportTask.GetComponent<transportTask>().numberOfCrates != int.Parse(split[1]))
        {
            foreach (Transform child in this.gameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            boxes = crates[transportTask.GetComponent<transportTask>().numberOfCrates - 1];
            Instantiate(boxes).transform.SetParent(this.gameObject.transform);


        }
    }
}
