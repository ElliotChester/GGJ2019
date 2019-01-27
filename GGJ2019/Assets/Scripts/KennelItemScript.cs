using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KennelItemScript : MonoBehaviour
{
    bool beingHeld = false;

    public LayerMask DraggableItem;
    public LayerMask Background;
    
    public Vector2 lastPosition;
    public Vector2 newPosition;

    public Sprite normalSprite;
    public Sprite HighlightedSprite;

    void Update()
    {
        if (beingHeld)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                beingHeld = false;
            }
        }
        else
        if (CheckForMouseOver() && Input.GetButtonDown("Fire1"))
        {
            beingHeld = true;
            foreach (Transform item in this.transform.parent)
            {
                if (item.tag == "Draggable Item")
                {
                    Debug.Log("settingg sorting order");
                    this.GetComponentInParent<CollectionControl>().MoveToTop(this.gameObject);
                }
            }
        }

    }

    private void LateUpdate()
    {
        if (beingHeld)
        {
            MoveWithCursor();
        }

        RotateWithMovement();

    }

    private void RotateWithMovement()
    {
        float newAngle = Mathf.LerpAngle(this.transform.eulerAngles.z, Mathf.Clamp((lastPosition.x - this.transform.position.x) * 100, -65, 65), 8 * Time.deltaTime);

        this.transform.eulerAngles = new Vector3(0, 0, newAngle);

        lastPosition = this.transform.position;
    }

    void MoveWithCursor()
    {
        Vector3 newPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        transform.position = Vector3.Lerp(transform.position, newPos, 15 * Time.deltaTime);
    }

    bool CheckForMouseOver()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000, DraggableItem);

        if (hit.collider != null && hit.transform == this.transform)
        {
            return true;
        }
        return false;
    }
}
