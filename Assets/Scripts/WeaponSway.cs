﻿using System;
using UnityEngine;
using Photon.Pun;
public class WeaponSway : MonoBehaviour {

    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float multiplier;
    public PhotonView PV;

    private void LateUpdate()
    {
	if(!PV.IsMine) {
		return;
	}
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;
	Quaternion newLocal = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
        // rotate 
        transform.localRotation = Quaternion.Lerp(newLocal, targetRotation, smooth * Time.deltaTime);
	//transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y + 270, transform.localRotation.z, transform.localRotation.w);
	
    }
}
