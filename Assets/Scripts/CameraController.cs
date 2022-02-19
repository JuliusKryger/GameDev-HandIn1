using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Transform pivot;
    public Vector3 offset;

    public bool useOffsetValue;
    public bool invertY;

    public float rotateSpeed;
    public float maxViewAngle;
    public float minViewAngle;

    void Start() {
        if (!useOffsetValue)
        {
           offset = target.position - transform.position; 
        }
        //  Pivot camera stuff.
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        //  Hiding cursor during gameplay.
        Cursor.lockState = CursorLockMode.Locked;
    }

    //  Using LateUpdate here as we only wanna move the camera after we have moved the player.
    void LateUpdate() {
        //  Gets the x posistion of the mouse & rotates the camera acordingly (Moves the camera from side to side).
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        //  Get Y position of the mouse and rotates the pivot.
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        if (invertY){
            pivot.Rotate(vertical, 0, 0);
        } else {
            pivot.Rotate(-vertical, 0, 0);
        }


        //  Limits the UP/DOWN camera rotation.
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle,0,0);
        }
         if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle,0,0);
        }

        //  Move the camera based on the current rotation of the target/pivot and the original offset.
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //  Prevents the player from looking through the ground with the camera.
        if (transform.position.y < target.position.y) {
            transform.position = new Vector3(transform.position.x, transform.position.y - 5f, transform.position.z);
        }

        //  Forces the camera to look at our player.
        transform.LookAt(target);
        //transform.position = target.position - offset;
    }
}