using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskUI : MonoBehaviour
{
    Player player;
    public int taskWindows = 0;
    public int tasks = 0;

 

    [SerializeField] GameObject window;

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
            win.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 980 - (200 * taskWindows));
            
            Instantiate(win).transform.SetParent(GameObject.Find("PlayerTaskBarSpawner").gameObject.transform);
            
        }
        updateWindows();

    }
    void updateWindows()
    {
        for (int i = 0; i < taskWindows; i++)
        {
            GameObject win = GameObject.FindGameObjectsWithTag("task")[i];
            Task task = player.tasks[i];
            win.transform.Find("PlayerTaskName").GetComponent<TextMeshProUGUI>().text = task.Name;
            win.transform.Find("frame").transform.Find("PlayerTaskOther").GetComponent<TextMeshProUGUI>().text = task.other;

        }
    }
}
