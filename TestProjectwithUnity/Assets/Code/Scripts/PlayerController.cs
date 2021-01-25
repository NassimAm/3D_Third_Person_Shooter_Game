using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Movement----------------------000000000000000000000000000000
    public float speed = 10.0f;
    public float jump_power = 5.0f;
    public float gravity = 9.8f;
    CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundDistance = 0.3f;

    Vector3 move;
    float movex, movez ;
    Vector3 velocity;
    bool isGrounded;
    bool jump;
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance , groundLayer);

        movex = Input.GetAxis("Horizontal");
        movez = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
            
            

        if (isGrounded)
        {
            velocity.y = 0.0f;
            if(jump)
            {
                velocity.y = jump_power;
                jump = false;
            }
        }

        if (!isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        
        move = (movex * transform.right + movez * transform.forward) * speed * Time.deltaTime;
        controller.Move(move);
        controller.Move(velocity * Time.deltaTime);


    }

}
