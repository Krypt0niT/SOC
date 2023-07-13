using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float sensX;
    float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    private void Start()
    {
        //nacitanie udajov zo scriptu settings na Managery
        sensX = GameObject.Find("Manager").GetComponent<Settings>().sensitivityX;
        sensY = GameObject.Find("Manager").GetComponent<Settings>().sensitivityY;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    private void Update()
    {
        if (GameObject.FindAnyObjectByType<Player>().cameraRotation)
        {
            //odchytenie inputu myši
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            //zabezpecenie maximalnej rozacie osi x
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);


            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
