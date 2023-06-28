using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    GameObject transportTask;
    GameObject boxes;
    List<GameObject> crates;
    public int type;

    private void Start()
    {
        

        
        transportTask = this.gameObject.transform.parent.transform.parent.gameObject;
        crates = transportTask.GetComponent<transportInfo>().crates;

        if (type == 0)
        {
            boxes = crates[transportTask.GetComponent<transportTask>().numberOfCrates0];
            Instantiate(boxes).transform.SetParent(this.gameObject.transform);
            print(boxes);
        }
        else if (type == 1)
        {
            if(transportTask.GetComponent<transportTask>().numberOfCrates1 - 1 < 0)
            {
                boxes = crates[0];
                Instantiate(boxes).transform.SetParent(this.gameObject.transform);
                return;
            }
            boxes = crates[transportTask.GetComponent<transportTask>().numberOfCrates1];
            Instantiate(boxes).transform.SetParent(this.gameObject.transform);
        }


    }
    private void Update()
    {
     
        cratesNumber();
    }
    void cratesNumber()
    {
        if (boxes == null) return;
        
        string[] split = boxes.name.Split('-'); 
        
        
        

        if (type == 0)
        {
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            foreach (Transform child in this.gameObject.transform)
            {
                child.transform.localPosition = new Vector3(0, 0, 0);
            }
            

            if (transportTask.GetComponent<transportTask>().numberOfCrates0 != int.Parse(split[1]))
            {
                foreach (Transform child in this.gameObject.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                boxes = crates[transportTask.GetComponent<transportTask>().numberOfCrates0];
                Instantiate(boxes).transform.SetParent(this.gameObject.transform);


            }
        }
        else if (type == 1)
        {
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            foreach (Transform child in this.gameObject.transform)
            {
                child.transform.localPosition = new Vector3(0, 0, 0);
            }
            

            if (transportTask.GetComponent<transportTask>().numberOfCrates1 != int.Parse(split[1]))
            {
                foreach (Transform child in this.gameObject.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                boxes = crates[transportTask.GetComponent<transportTask>().numberOfCrates1];
                Instantiate(boxes).transform.SetParent(this.gameObject.transform);


            }
        }
        
        
    }
}
