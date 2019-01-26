using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    

    public float cameraSpeed;
    public float cameraAcceleration;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody RB = Camera.main.GetComponent<Rigidbody>();

        Vector3 newVelocity = RB.velocity;

        if (Camera.main.WorldToScreenPoint(this.transform.position).x > Screen.width * 0.7f)
        {
            newVelocity.x = Mathf.Lerp(newVelocity.x, 1 * cameraSpeed, cameraAcceleration * Time.deltaTime);
        }
        else if(Camera.main.WorldToScreenPoint(this.transform.position).x < Screen.width * 0.3f)
        {
            newVelocity.x = Mathf.Lerp(newVelocity.x, -1 * cameraSpeed, cameraAcceleration * Time.deltaTime);
        }
        else
        {
            newVelocity.x = Mathf.Lerp(newVelocity.x, 0, cameraAcceleration * Time.deltaTime);
        }

        if (Camera.main.WorldToScreenPoint(this.transform.position).y > Screen.height * 0.7f)
        {
            newVelocity.z = Mathf.Lerp(newVelocity.z, 1 * cameraSpeed, cameraAcceleration * Time.deltaTime);
        }
        else if (Camera.main.WorldToScreenPoint(this.transform.position).y < Screen.height * 0.3f)
        {
            newVelocity.z = Mathf.Lerp(newVelocity.z, -1 * cameraSpeed, cameraAcceleration * Time.deltaTime);
        }
        else
        {
            newVelocity.z = Mathf.Lerp(newVelocity.z, 0, cameraAcceleration * Time.deltaTime);
        }

        RB.velocity = newVelocity;
    }
}
