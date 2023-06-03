using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("Information")]
    public bool interactable = false;
    public bool hasTask = false;
    public bool playerInRange = false;

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material interactableMaterial;
    [SerializeField] Material taskMaterial;


    private void Update()
    {
        taskFinder();
        changeColor();
    }
    void taskFinder()
    {
        if (gameObject.GetComponent<Task>() != null)
        { hasTask = true; }
        else
        { hasTask = false; }
    }
    void changeColor()
    {
        if (hasTask)
        {
            gameObject.transform.Find("human model").GetComponent<Renderer>().material = taskMaterial;
            return;
        }
        if (interactable)
        {
            gameObject.transform.Find("human model").GetComponent<Renderer>().material = interactableMaterial;
            return;
        }
        gameObject.transform.Find("human model").GetComponent<Renderer>().material = defaultMaterial;

    }
}
