using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //public Rigidbody theRB;
    public CharacterController controller;
    public Animator anim;
    private Vector3 moveDirection;
    public Transform pivot;
    public GameObject playerModel;

    public float rotateSpeed;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;

    public float KnockBackForce;
    public float KnockBackTime;
    private float KnockBackCounter;

    //Physics.gravity = new Vector3(0, -9.8F, 0);
    

    void Start() {
        //theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        if(KnockBackCounter <= 0) {
        //  We wanna store our Y-Direction in a float so that when we normalize moveDirection it won't makes us slow fall.
        float yStore = moveDirection.y;
        //  Makes us move the direction camera is facing when going forward.
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        //  Normalizing the speed, as we either moves really slow or really fast whithout it.
        moveDirection = moveDirection.normalized * moveSpeed;
        //  Then setting our moveDirection.y as it were before we normalized.
        moveDirection.y = yStore;

        //  Checks if we're grounded and if the (spacebar) is pressed we Jump, this prevents double jumping.
        //  also defaults our y direction to zero as soon as we're grounded.
        if (controller.isGrounded) {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump")) {
                moveDirection.y = jumpForce;   
            }
        }
        } else {
            KnockBackCounter -= Time.deltaTime;
        }

        //  Adding Physics to our player. (Making him gravitate towards the ground).
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        //  OBS. We are using "Time.deltatime" to eliminate the difference of a users speed based on their framerate. Beacuse "Time.deltatime" is (The interval in seconds from the last frame to the current one).
        controller.Move(moveDirection * Time.deltaTime);

        //  Move the player in different directions based on a camera look direction.
        if (Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") !=0) {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);

            //  Smoothes things out a bit.
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }

    public void Knockback(Vector3 direction) {
        KnockBackCounter = KnockBackTime;
        moveDirection = direction * KnockBackForce;
        moveDirection.y = KnockBackForce;
    }
}


    //  The Movement (awsd keys).
    //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);


// This is now redundent code that i used for the player when using the rigidbody movement system. (Belonged in update function)

        /*  This is the line that dertimines the speed of our player and it's movement based on a Vector3(x-axis, y-axis, z-axis).  */
        /*  The keys a & d is bound to Horizontal movement and the keys w & s i bound to Vertical movement.                         */
        /*  Note that we're leaving the y space of the Vector3 alone as we don't want any jump function in this line.               */
        //theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        /*if (Input.GetButtonDown("Jump")) {
            theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        }*/