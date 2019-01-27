using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour
{
    int selectedObject = 0;

    float cooldown;

    public List<GameObject> selectableObjects;

    public float moveSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(Input.GetAxisRaw("DPADHorizontal"));

        SelectItem();

        MoveItem();

    }

    private void MoveItem()
    {
        selectableObjects[selectedObject].transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed, 0);
    }

    void SelectItem()
    {
        if (cooldown <= 0)
        {
            if (Input.GetAxisRaw("DPADHorizontal") < -0.5f)
            {
                selectedObject--;
                if (selectedObject < 0) { selectedObject = selectableObjects.Count - 1; }
            }

            if (Input.GetAxisRaw("DPADHorizontal") > 0.5f)
            {
                selectedObject++;
                if (selectedObject >= selectableObjects.Count) { selectedObject = 0; }
            }

            foreach (var item in selectableObjects)
            {
                item.GetComponentInChildren<SpriteRenderer>().sprite = item.GetComponent<KennelItemScript>().normalSprite;
            }

            selectableObjects[selectedObject].GetComponentInChildren<SpriteRenderer>().sprite = selectableObjects[selectedObject].GetComponent<KennelItemScript>().HighlightedSprite;

            cooldown = 0.1f;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
}
