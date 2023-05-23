using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainUI : MonoBehaviour
{

    TextMeshProUGUI debugspeed;
    // Start is called before the first frame update
    void Start()
    {
        debugspeed = GameObject.Find("debug speed").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        debugspeed.text = "speed: " + GameObject.Find("Player").GetComponent<Rigidbody>().velocity.ToString();
    }
}
