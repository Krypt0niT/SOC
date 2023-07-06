using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carriedObjectIdentifier : MonoBehaviour
{
    public GameObject transportObj;
    public bool carried = false;
    private void Start()
    {
        if(transform.parent.name == "orientation") { return; }
        transportObj = transform.parent.parent.parent.parent.gameObject;
    }
}
