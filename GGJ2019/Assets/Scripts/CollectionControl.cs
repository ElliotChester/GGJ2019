using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionControl : MonoBehaviour
{
    GameObject selectedObject;
    int selectedObjectint = 0;

    float cooldown;

    public List<GameObject> selectableObjects;

    List<GameObject> sortOrder;

    public float moveSpeed;

    void Start()
    {
        sortOrder = selectableObjects;
    }

    void Update()
    {
        Debug.Log(Input.GetAxisRaw("DPADHorizontal"));




        SelectItemWithController();

        MoveWithController();

    }

    public void MoveToTop(GameObject itemToMove)
    {
        for (int i = 0; i < sortOrder.Count; i++)
        {
            if(sortOrder[i] == itemToMove)
            {
                sortOrder.Add(sortOrder[i]);
                sortOrder.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < sortOrder.Count; i++)
        {
            sortOrder[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = i;
            sortOrder[i].transform.position = new Vector3(sortOrder[i].transform.position.x, sortOrder[i].transform.position.y, i * -0.1f);
        }
    }

    private void MoveWithController()
    {
        selectedObject.transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed, 0);
    }

    void SelectItemWithController()
    {
        if (cooldown <= 0)
        {
            if (Input.GetAxisRaw("DPADHorizontal") < -0.5f)
            {
                selectedObjectint--;
                if (selectedObjectint < 0) { selectedObjectint = selectableObjects.Count - 1; }
            }

            if (Input.GetAxisRaw("DPADHorizontal") > 0.5f)
            {
                selectedObjectint++;
                if (selectedObjectint >= selectableObjects.Count) { selectedObjectint = 0; }
            }

            foreach (var item in selectableObjects)
            {
                item.GetComponentInChildren<SpriteRenderer>().sprite = item.GetComponent<KennelItemScript>().normalSprite;
            }

            selectedObject.GetComponentInChildren<SpriteRenderer>().sprite = selectedObject.GetComponent<KennelItemScript>().HighlightedSprite;

            cooldown = 0.2f;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
}
