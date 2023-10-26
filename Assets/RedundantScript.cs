using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RedundantScript : MonoBehaviour
{
      public Transform orientation;
	float xRotation;
	float yRotation;
	public float speed;
	public PhotonView PV;
	void Start() {
		
		PV = GetComponentInParent<PhotonView>();
	}
    
	void Update () {
		if(!PV.IsMine) {
			return;
		}
		float mouseY = -Input.GetAxis("Mouse X") * speed;
		float mouseX = -Input.GetAxis("Mouse Y") * speed;
		
		xRotation += mouseX;
		yRotation += -mouseY;
		
		xRotation = Mathf.Clamp(xRotation, -60f, 60f);
		
		orientation.rotation = Quaternion.Euler(0, yRotation, 0);
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		
		Debug.DrawRay(transform.position, transform.forward);
	}
 
    

 
    
}