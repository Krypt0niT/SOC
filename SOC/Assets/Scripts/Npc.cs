using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

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

    TextMeshPro symbol;

    public bool interacting = false;
    GameObject player;

    //movement
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    Animator animator;

    private void Start()
    {
        symbol = this.gameObject.transform.Find("symbol").GetComponent<TextMeshPro>();
        agent = GetComponent<NavMeshAgent>();
        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area  
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            agent.SetDestination(point);
        }
        player = GameObject.FindObjectOfType<Player>().gameObject;
        animator = transform.Find("model").GetComponent<Animator>();
    }



    private void Update()
    {
        taskFinder();
        convoChange();
        symbolUpdate();

        
        
        movement();
        
        



    }
    void movement()
    {
        Vector3 point;
        if (interacting)
        {
            point = this.transform.position;
            agent.SetDestination(point);
            transform.LookAt(player.transform);
            transform.rotation = Quaternion.Euler(
            0,
            transform.rotation.eulerAngles.y,
            0
            );
            animator.SetBool("walking", false);
            return;

        }
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            
            
            
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area  
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
                animator.SetBool("walking", true);
            }
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    void symbolUpdate()
    {
        symbol.gameObject.transform.LookAt(GameObject.FindObjectOfType<Player>().gameObject.transform);
        symbol.gameObject.transform.rotation = Quaternion.Euler(
            0,
            symbol.gameObject.transform.rotation.eulerAngles.y + 180,
            symbol.gameObject.transform.rotation.eulerAngles.z
            );
        if (hasTask)
        {
            symbol.text = "?";
            return;
        }
        if (interactable)
        {
            symbol.text = "!";
            return;
        }
        symbol.text = "";
    }
    void taskFinder()
    {
        if (gameObject.GetComponent<Task>() != null)
        { hasTask = true; }
        else
        { hasTask = false; }
    }
    void convoChange()
    {
        if (conversation.Count != 0)
        {
            if (!hasTask)
            {
                if(interactiveIndex > conversation.Count - 1)
                {
                    return;
                }
                interctiveText = conversation[interactiveIndex];
            }
            else
            {
                if (conversation.Count == interactiveIndex)
                {
                    interctiveText = "";
                    if (GameObject.FindObjectOfType<Player>().playerInteracting)
                    {
                        GameObject.FindAnyObjectByType<MainUI>().TaskBarShow();
                    }
                    
                }
                else
                {
                    interctiveText = conversation[interactiveIndex];
                }
            }
        }
        if (!GameObject.FindAnyObjectByType<Player>().playerInteracting)
        {
            GameObject.FindAnyObjectByType<MainUI>().TaskBarHide();
        }
    }
    
}
