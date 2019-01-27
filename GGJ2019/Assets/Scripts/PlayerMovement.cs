using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody RB;
    Animator Anim;

    public float speed;

    void Start()
    {
        RB = this.GetComponent<Rigidbody>();
        Anim = this.GetComponent<Animator>();
    }

    void LateUpdate()
    {
        Move();
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



    void Move()
    {
        RB.velocity = Vector3.Lerp(RB.velocity, new Vector3(Input.GetAxisRaw("Horizontal") * speed, RB.velocity.y, Input.GetAxisRaw("Vertical") * speed * 1.5f), 15 * Time.deltaTime);

        if (PlayerController.instance.hasParachute && PlayerController.instance.carryingKennel && !IsGrounded() && RB.velocity.y < 0 && Input.GetButton("Jump"))
        {
            RB.useGravity = false;
            float newDownSpeed = Mathf.Lerp(RB.velocity.y, -1, 15 * Time.deltaTime);
            RB.velocity = new Vector3(RB.velocity.x, newDownSpeed, RB.velocity.z);
            PlayerController.instance.Parachute.GetComponentInChildren<SpriteRenderer>().sprite = PlayerController.instance.ParachuteOpen;
        }
        else
        {
            RB.useGravity = true;
            PlayerController.instance.Parachute.GetComponentInChildren<SpriteRenderer>().sprite = PlayerController.instance.ParachuteClosed;
        }

        Anim.SetFloat("Speed", RB.velocity.magnitude);
        Anim.SetBool("Grounded", IsGrounded());
        if(RB.velocity.x < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            if (PlayerController.instance.carryingKennel)
            {
                PlayerController.instance.Kennel.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);

            if (PlayerController.instance.carryingKennel)
            {
                PlayerController.instance.Kennel.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            RB.velocity = RB.velocity + (Vector3.up * 5);
            Anim.SetTrigger("Jump");
        }
    }
}
