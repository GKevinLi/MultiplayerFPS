using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
	//public Rigidbody rb;
	public Transform cameraPos;
	void Start() {
		//Screen.lockCursor = true;
	}
    
	void FixedUpdate () {
		//rb.MovePosition(cameraPos.position);
		transform.position = cameraPos.position;
	}
}
