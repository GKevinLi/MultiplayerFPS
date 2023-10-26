using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LookAtMouse : MonoBehaviour
{
      public Transform orientation;
	float xRotation;
	float yRotation;
	public float speed;
	public PhotonView PV;
	void Start() {
		if(PV.IsMine) {
			Screen.lockCursor = true;
			Cursor.visible = false;
		}
	}
    
	void Update () {
		float mouseY = -Input.GetAxisRaw("Mouse X") * speed;
		float mouseX = -Input.GetAxisRaw("Mouse Y") * speed;
		xRotation += mouseX;
		yRotation += -mouseY;
		xRotation = Mathf.Clamp(xRotation, -60f, 60f);
		orientation.rotation = Quaternion.Euler(0, yRotation, 0);
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		//if(Physics.Raycast(transform.position, transform.forward, 10f)) {
			//transform.Translate(0, 0, -0.1f);
		//}
		Debug.DrawRay(transform.position, transform.forward);
	}
 
    

 
    
}
