using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KennelScript : MonoBehaviour
{

    public float fallSpeed;

    void Update()
    {
        if (!IsGrounded())
        {
            this.transform.position -= Vector3.up * Time.deltaTime * fallSpeed;
        }
    }

    bool IsGrounded()
    {
        Debug.DrawRay(this.transform.position, Vector3.down, Color.red, 5);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, Vector2.down, out hit, 0.75f))
        {
            return true;
        }

        return false;
    }
}
