using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public GameObject cameraPosition;
    private void Update()
    {
        transform.position = cameraPosition.transform.position;
    }
}
