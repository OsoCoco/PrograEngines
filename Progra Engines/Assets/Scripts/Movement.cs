using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    IDLE,
    WALKING,
    DASHING,
}
public class Movement : MonoBehaviour
{

    CharacterController controller;
    public PlayerState myState;
    Vector3 move,playerVelocity,targetDir;
    Vector3 dashV = Vector3.forward;
    float xAxis, zAxis,gravity;
    public float moveSpeed, dashSpeed, dashTime, rotationSpeed,resetTime;

    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        myState = PlayerState.IDLE;
        gravity = -9.8f;
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is calles once per frame
    void Update()
    {
        grounded = controller.isGrounded;

        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");

        move = new Vector3(xAxis, 0, zAxis);

        if (grounded && playerVelocity.y < 0)
        {
            move.y = 0;
        }

       
        playerVelocity.y += gravity * Time.deltaTime;
        
        switch (myState)
        {
            case PlayerState.IDLE:
                controller.Move(playerVelocity * Time.deltaTime);
                if (move != Vector3.zero)
                {
                    myState = PlayerState.WALKING;
                }
               
                break;

            case PlayerState.WALKING:
                controller.Move(move * moveSpeed * Time.deltaTime);

                Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

                dashV = targetDir - transform.position;

                if(move == Vector3.zero)
                {
                    myState = PlayerState.IDLE;
                }
                
               
                Debug.DrawLine(transform.position, transform.position + move,Color.red);
                break;

            case PlayerState.DASHING:
                    Dash();
                break;
            default:
                break;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (myState != PlayerState.DASHING)
                myState = PlayerState.DASHING;
        }
    }

    private void LateUpdate()
    {
        targetDir = transform.position + move * 2f;
    }
    void Dash()
    {
        controller.Move(dashV * dashSpeed * Time.deltaTime);

        if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
        }
        else
        {
            myState = PlayerState.IDLE;
            dashTime = resetTime;
        }
    }
}
