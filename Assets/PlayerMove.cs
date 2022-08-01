using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerMove : NetworkBehaviour
{
    public float speed = 25.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;
    public float sensitivity = 5;
    private CharacterController controller;
    private Transform cam;


    // Use this for initialization
    void Start()
    {
        //controls
        controller = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(LateStartGrabCamera());
    }

    private IEnumerator LateStartGrabCamera()
    {
        yield return new WaitForEndOfFrame();
        cam = transform.Find("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            // is the controller on the ground?
            if (controller.isGrounded)
            {
                //Feed moveDirection with input.
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                //Multiply it by speed.
                moveDirection *= speed;
                //Jumping
                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;

            }
            turner = Input.GetAxis("Mouse X") * sensitivity;
            looker = -Input.GetAxis("Mouse Y") * sensitivity;
            if (turner != 0)
            {
                //horizontal  move
                transform.eulerAngles += new Vector3(0, turner, 0);
            }
            if (looker != 0)
            {
                if(cam.rotation.x < 45f && cam.rotation.x > -45f)
                {
                    cam.eulerAngles += new Vector3(looker, 0, 0);
                    //cam.rotation = new Quaternion(cam.rotation.w, cam.rotation.x + looker * 0.01f, cam.rotation.y, cam.rotation.z);
                }
            }
            //Applying gravity to the controller
            moveDirection.y -= gravity * Time.deltaTime;
            //Making the character move
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}