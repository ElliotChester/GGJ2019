using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrievalBox : MonoBehaviour
{
    public GameObject Kennel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retrieve()
    {
		PlayerController.instance.PickupKennel();
        //Kennel.transform.position = this.transform.position + Vector3.back + Vector3.up * 2;
    }
}
