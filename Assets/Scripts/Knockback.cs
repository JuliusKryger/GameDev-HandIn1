using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {
void OnCollisionEnter(Collision collision)
{
Debug.Log(collision.gameObject.name);
if (collision.gameObject.tag == "Player")
{
//hmmm here we need to add some push effect to the player.
}
}
void OnCollisionStay(Collision collision) { }
void OnCollisionExit(Collision collision) { }
}