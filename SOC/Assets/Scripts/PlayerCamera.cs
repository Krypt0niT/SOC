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
        //odchytenie inputu myši
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        //zabezpecenie maximalnej rozacie osi x
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
