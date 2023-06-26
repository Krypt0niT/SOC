using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskUI : MonoBehaviour
{
    Player player;
    public int taskWindows = 0;
    public int tasks = 0;

 

    public GameObject window;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        

    }
    private void Update()
    {
        taskWindows = GameObject.FindGameObjectsWithTag("task").Length;
        tasks = player.tasks.Count;
        if (tasks > taskWindows)
        {
            GameObject win = window;
            Instantiate(win).transform.SetParent(GameObject.Find("PlayerTaskBarSpawner").gameObject.transform);
            
        }
        if ( tasks == taskWindows)
        {
            if (tasks != 0)
            {
                GameObject[] win = GameObject.FindGameObjectsWithTag("task");
                for (int i = 0; i < win.Length; i++)
                {
                    win[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100 - (i * 200));

                }
            }
            

        }

        updateWindows();

    }
    void updateWindows()
    {
        if (GameObject.FindGameObjectsWithTag("task").Length != player.tasks.Count) { return; }
        for (int i = 0; i < taskWindows; i++)
        {
            GameObject win = GameObject.FindGameObjectsWithTag("task")[i];
            Task task = player.tasks[i];
            win.transform.Find("PlayerTaskName").GetComponent<TextMeshProUGUI>().text = task.Name;
            win.transform.Find("frame").transform.Find("PlayerTaskOther").GetComponent<TextMeshProUGUI>().text = task.other;

      


            //time
            int secs = 0;
            int mins = 0;
            if (task.time > 59)
            {
                secs = task.time % 60;
                mins = task.time / 60;
            }
            else
            {
                secs = task.time;
            }
            if(secs.ToString().Length == 1)
            {
                win.transform.Find("frame").transform.Find("PlayerTaskTimer").GetComponent<TextMeshProUGUI>().text = mins + " : 0" + secs;
            }
            else
            {
                win.transform.Find("frame").transform.Find("PlayerTaskTimer").GetComponent<TextMeshProUGUI>().text = mins + " : " + secs;

            }



        }

    }
    
}
