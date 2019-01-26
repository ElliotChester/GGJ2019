using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableScript : MonoBehaviour
{
    public Vector3 AttachOffset;

    public bool GoToSpot = false;

    public bool AtTargetSpot = false;

    private void Update()
    {
        if (!GoToSpot) { return; }
        if (!AtTargetSpot)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, AttachOffset, 5 * Time.deltaTime);
            if(Vector3.Distance(this.transform.localPosition, AttachOffset) < 0.1f)
            {
                AtTargetSpot = true;
            }
        }
        else
        {
            this.transform.localPosition = AttachOffset;
        }
    }

}
