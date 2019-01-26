using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour
{
    public Light light;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DarkZone")
        {
            light.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DarkZone")
        {
            light.enabled = false;
        }
    }
}
