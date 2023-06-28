using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private float timeTick = 0;
    public bool taskTimeCounter = true;
    public float playerMoney = 0;
    private void Update()
    {
        taskTimeUpdate();
    }
    void taskTimeUpdate()
    {
        if(!taskTimeCounter) { return; }

        timeTick += Time.deltaTime;
        if (timeTick < 1) { return; }
        timeTick = 0;

        Task[] tasks = GameObject.FindObjectsOfType<Task>();

        for (int i = 0; i < tasks.Length; i++)
        {
            if (!tasks[i].taken) { continue; }
            tasks[i].time--;
        }
    }
}
