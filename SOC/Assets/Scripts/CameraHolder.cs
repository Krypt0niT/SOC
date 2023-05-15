using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public GameObject cameraPosition;
    private void Update()
    {
        //prenazanie CameraHoldera na poziciu hraca
        transform.position = cameraPosition.transform.position;
    }
}
