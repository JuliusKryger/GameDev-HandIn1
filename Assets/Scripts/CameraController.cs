using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    public bool useOffsetValue;

    public float rotateSpeed;

    void Start() {
        if (!useOffsetValue)
        {
           offset = target.position - transform.position; 
        }
    }

    void Update() {
        //  Gets the x posistion of the mouse & rotates the camera acordingly (Moves the camera from side to side).
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        //  Moves the camera up and down.
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        target.Rotate(-vertical, 0, 0);

        //  Move the camera based on the current rotation of the target and the original offset.
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = target.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //  Forces the camera to look at our player.
        transform.LookAt(target);
        //transform.position = target.position - offset;
    }
}