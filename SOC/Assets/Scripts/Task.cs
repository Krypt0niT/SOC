using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    [Header("General")]
    public string Name = "Prenos krabíc";
    public bool taken = false;
    public bool completed = false;

    [Header("Ecomomy")]
    public bool paid = true;
    public float money = 1.5f;

    [Header("Timer")]
    public int timeToFinish = 60;
    public int time = 0;


}
