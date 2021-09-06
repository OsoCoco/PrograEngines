using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    bool grounded;
    CharacterController controller;
    FOV fov;
    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FOV>();
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded;

        if (fov.canSeeTraget)
            FollowPlayer(fov.targetRef.transform);
    }

    void FollowPlayer(Transform target)
    {
        var offset = target.position - transform.position;
        
        //Get the difference.
        if (offset.magnitude > .1f)
        {
            offset = offset.normalized * moveSpeed;
            controller.Move(offset * Time.deltaTime);
            Quaternion toRotation = Quaternion.LookRotation(offset, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
