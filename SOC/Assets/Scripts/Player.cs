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


    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    int taskCapacity = 0;
    public List<Task> tasks = new List<Task>();




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        //raycast kontrola ci je hrac na zemi
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        speedControl();
        myInput();


        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        
        

        
    }
    private void FixedUpdate()
    {
        movePLayer();
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
    public GameObject getAimedObject()
    {
        // Vytvoríme raycast z kamery na stred obrazovky
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        // Kontrola, či raycast zasiahne nejaký objekt
        if (Physics.Raycast(ray, out hit))
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
}
