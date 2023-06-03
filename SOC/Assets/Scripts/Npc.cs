using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("Information")]
    public bool interactable = false;
    public bool hasTask = false;
    public bool playerInRange = false;

    [Header("Conversation")]
    public string interctiveText = "";
    public int interactiveIndex = 0;
    public List<string> conversation = new List<string>();
    





    [SerializeField] Material defaultMaterial;
    [SerializeField] Material interactableMaterial;
    [SerializeField] Material taskMaterial;
    private void Start()
    {
        //naplnenie interakcie (mozne z editoru)
        conversation.Add("Ahoj! Som tvoj virtuálny sprievodca v tomto školskom simulátore." +
            " Vždy keď ma potrebuješ, tu som pre teba. Neboj sa pýtať, rád ti pomôžem objaviť svet vzdelávania a zábavy!");
        conversation.Add("Rada sa stávam súčasťou tvojej hry!" +
            " Ak budeš mať akékoľvek ďalšie otázky alebo potrebuješ ďalšiu pomoc, neváhaj sa mi ozvať." +
            " Zábavu a úspech vo virtuálnom svete školy!");
        conversation.Add("Ďakujem, že si si vybral tento školský simulátor! Verím, že spolu prežijeme úžasné dobrodružstvá" +
            " v prostredí školy.Ak sa cítiš stratený alebo potrebuješ radu, vždy ma tu nájdeš.Nech žiari tvoje vzdelanie v tejto simulácii!");

    }

    private void Update()
    {
        taskFinder();
        changeColor();
        interctiveText = conversation[interactiveIndex];
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
