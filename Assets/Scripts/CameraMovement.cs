using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CameraMovement : MonoBehaviour
{
	public Transform newCamPos;
	public Transform cameraPos;
	public Transform sniperCamPos;
	public IKController thing;
	public GameObject scopeCanvas;
	public GameObject crosshair;
	public PhotonView PV;
	public bool sniperActive;

        public GameObject sniper;
	void Start() {
		
	}
    
	void Update () {
		if(!PV.IsMine) {
			return;
		}
		if(sniper.activeSelf) {
			sniperActive = true;
		}
		if(!sniper.activeSelf) {
			sniperActive = false;
		}
		
		if(Input.GetMouseButton(1)) {
			transform.position = newCamPos.position;
			
			if(sniperActive) {
				
				transform.position = sniperCamPos.position;
				scopeCanvas.SetActive(true);
	
				crosshair.SetActive(false);
				
	
			}
			else {
				scopeCanvas.SetActive(false);
				crosshair.SetActive(true);
			}
		}
		else {
		    transform.position = cameraPos.position;
			
			scopeCanvas.SetActive(false);
			crosshair.SetActive(true);
			
		}
	}
	
}