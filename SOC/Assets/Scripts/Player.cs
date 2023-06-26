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
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * PlayerSpeed * 10f, ForceMode.Force);
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

            if (getAimedObject() == null) { return; }
            if (getAimedObject().transform.parent.GetComponent<Npc>() == null) { return; }
            if (!getAimedObject().transform.parent.GetComponent<Npc>().playerInRange) { return; }
            if (!getAimedObject().transform.parent.GetComponent<Npc>().interactable) { return; }
            if (getAimedObject().tag != "npc") { return; }
            if (!playerInteracting) 
            {
                cameraRotation = false;
                playerInteracting = true;

            }
            else
            {
                cameraRotation = true;
                playerInteracting = false;
                GameObject.Find("Player").GetComponent<Player>().getAimedObject()
                    .transform.parent.GetComponent<Npc>().interactiveIndex = 0;
            }
        }
    }
    public GameObject getAimedObject()
    {
        LayerMask layerMask = LayerMask.GetMask("NPCs");
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
        GameObject.FindObjectOfType<MainUI>().LoseTask();
    }
}
