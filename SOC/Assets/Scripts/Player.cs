using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float PlayerSpeed;
    public float groundDrag;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public Transform orientation;

    //settings / controls
    Settings settings;

    float horizontalInput;
    float verticalInput;

    public bool playerInteracting = false;
    public bool cameraRotation = true;

    Vector3 moveDirection;

    Rigidbody rb;

    public int taskCapacity = 3;
    public List<Task> tasks = new List<Task>();

    //box task
    bool carring = false;
    public GameObject carringObj = null;

    public float Slower = 1;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        settings = GameObject.Find("Manager").GetComponent<Settings>();
    }
    private void Update()
    {
        //raycast kontrola ci je hrac na zemi
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        speedControl();
        myInput();
        Grounded();
        interactControll();
        taskTimeControll();

        boxCarring();
        NpcStopper();




        if (carring)
        {
            Slower = 2;
        }
        else
        {
            Slower = 1;
        }

    }
    private void FixedUpdate()
    {
        movePLayer();
    }
    void Grounded()
    {
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void movePLayer()
    {
        float speed = PlayerSpeed / Slower;
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }
    private void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > PlayerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * PlayerSpeed;
            rb.velocity = new Vector3(limitedVel.x,rb.velocity.y, limitedVel.z);
        }
    }
    private void interactControll()
    {
        if (Input.GetKeyDown(settings.interactKey))
        {
            GameObject obj = getAimedObject();

            if (obj == null) { return; }
            if (obj.transform.parent == null) { return; }
            if (obj.transform.parent.GetComponent<Npc>() == null) { return; }
            if (!obj.transform.parent.GetComponent<Npc>().playerInRange) { return; }
            if (!obj.transform.parent.GetComponent<Npc>().interactable) { return; }
            if (obj.tag != "npc") { return; }
            if (!playerInteracting) 
            {
                cameraRotation = false;
                playerInteracting = true;
                obj.transform.parent.GetComponent<Npc>().interacting = true;

            }
            else
            {
                cameraRotation = true;
                playerInteracting = false;
                obj.transform.parent.GetComponent<Npc>().interactiveIndex = 0;
            }
        }
    }
    void NpcStopper()
    {
        if (!GameObject.FindObjectOfType<MainUI>().interactiveBar.activeSelf) return;
        if (getAimedObject() == null) return;
        if (getAimedObject().transform.parent == null) return;
        if (getAimedObject().transform.parent.GetComponent<Npc>() == null) return;
        getAimedObject().transform.parent.GetComponent<Npc>().interacting = true;
    }
    public void hideInteractiveBar()
    {
      
        cameraRotation = true;
        playerInteracting = false;
        
    }
    public GameObject getAimedObject()
    {
        LayerMask layerMask = LayerMask.GetMask("NPCs","boxes","boxPlates");
        // Vytvoríme raycast z kamery na stred obrazovky
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        // Kontrola, či raycast zasiahne nejaký objekt
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // Získame informácie o zasiahnutom objekte
            return hit.transform.gameObject;
            

            // Tu môžete vykonať akcie na základe zasiahnutého objektu
            // Napríklad môžete ho označiť, spustiť interakciu alebo získať ďalšie informácie.
        }
        else
        {
            return null;
        }

    }
    void taskTimeControll()
    {
        GameObject.FindObjectOfType<MainUI>().LoseTaskTime();
    }
    void boxCarring()
    {
        if (Input.GetKeyDown(settings.interactKey))
        {
            
            GameObject obj = getAimedObject();
            if(obj == null) { return; }
            

            if (obj.gameObject.tag == "box")
            {
                if (carring) { return; }
                //kontrola nachadzania sa v range
                if (obj.transform.parent.parent.parent.parent.gameObject.GetComponent<transportTask>().playerInRange !=
                    obj.gameObject.transform.parent.parent.parent.gameObject) { return; }



                    carring = true;
                if (obj.transform.parent.parent.GetComponent<Boxes>().type == 0)
                {
                    obj.transform.parent.parent.parent.parent.gameObject.GetComponent<transportTask>().numberOfCrates0--;

                }
                else if (obj.transform.parent.parent.GetComponent<Boxes>().type == 1)
                {
                    obj.transform.parent.parent.parent.parent.GetComponent<transportTask>().numberOfCrates1--;

                }
                obj.GetComponent<carriedObjectIdentifier>().carried = true;
                Instantiate(obj).transform.parent = this.gameObject.transform.Find("orientation").transform;
                foreach (Transform child in this.gameObject.transform.Find("orientation").transform)
                {
                    carringObj = child.gameObject;
                }
            }
            if(obj.gameObject.tag == "boxPlate")
            {

                if (!carring) { return; }

                if (carringObj == null) { return; }

                if (carringObj.tag != "box") { return; }
                if (obj.transform.parent.parent.gameObject.GetComponent<transportTask>() == null) return;
                if (obj.transform.parent.parent.gameObject.GetComponent<transportTask>().playerInRange !=
                    obj.gameObject.transform.parent.gameObject) { return; }
                if (carringObj.GetComponent<carriedObjectIdentifier>().transportObj != obj.transform.parent.parent.gameObject) return;
                
                if (obj.transform.parent.transform.parent.gameObject.GetComponent<transportTask>() != null)
                {
                    carringObj = null;
                    carring = false;
                    if (obj.transform.parent.GetComponentInChildren<Boxes>().type == 0)
                    {
                        obj.transform.parent.parent.GetComponent<transportTask>().numberOfCrates0++;

                    }
                    else if (obj.transform.parent.GetComponentInChildren<Boxes>().type == 1)
                    {
                        obj.transform.parent.parent.GetComponent<transportTask>().numberOfCrates1++;

                    }
                    foreach (Transform child in this.gameObject.transform.Find("orientation").transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                }
                
                

            }
        }
        if(carring)
        {
            
            foreach (Transform child in this.gameObject.transform.Find("orientation").transform)
            {
                child.transform.localPosition = new Vector3(0, 0.24f, 0.3f);
                child.transform.localRotation = Quaternion.Euler(0, 0, 0);
                child.gameObject.layer = 0;
            }
        }
       


    }
}
