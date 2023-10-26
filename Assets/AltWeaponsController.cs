using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class AltWeaponsController : MonoBehaviourPunCallbacks
{
    public bool toggle;
    private bool canThrow;

    public float time;
    public bool playingAnim;
    public Animator anim;
    public PhotonView PV;
    public Transform oldPos;
    public GameObject itemPos;

    [Header("Right Hand IK")]
    [Range(0, 1)] public float rightHandWeight;
    private Transform rightHandObj = null;
    public Transform rightHandHint = null;

    [Header("Left Hand IK")]
    [Range(0, 1)] public float leftHandWeight;
    private Transform leftHandObj = null;
    public Transform leftHandHint = null;

    public GameObject[] consumables;
    public GameObject activeItem;
    public int mouseMovement;

    // Start is called before the first frame update
    void Start()
    {
        toggle = false;
	playingAnim = false;
	anim = GetComponent<Animator>();
	activeItem = consumables[0];
        rightHandObj = activeItem.transform.Find("RightHandPos");
	leftHandObj = activeItem.transform.Find("LeftHandPos");
	canThrow = true;
	
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) {
		toggle = !toggle;
	}
	if(toggle != true) {
		photonView.RPC("RPC_DisableGuns2", RpcTarget.AllBuffered);
		photonView.RPC("RPC_DisableGun2", RpcTarget.AllBuffered);

	}
	if(toggle == true) {
		if(Input.GetAxisRaw("Mouse ScrollWheel") != 0 && !Input.GetMouseButton(1)) {
		if(Input.GetAxisRaw("Mouse ScrollWheel") > 0 && !playingAnim ) {
  			
			if(PV.IsMine) {
				mouseMovement = mouseMovement + 1;
				Hashtable hash = new Hashtable();
				hash.Add("itemIndex", mouseMovement);
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
				photonView .RPC("RPC_UpdateMouseMovement2", RpcTarget.AllBuffered, mouseMovement);
			}
			
		}
		if(Input.GetAxisRaw("Mouse ScrollWheel") < 0  && canThrow) {
			if(mouseMovement == 0) { 
				if(PV.IsMine) {
					mouseMovement = consumables.Length -1;
					Hashtable hash = new Hashtable();
					hash.Add("itemIndex", mouseMovement);
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);						photonView.RPC("RPC_UpdateMouseMovement2", RpcTarget.AllBuffered, mouseMovement);
					}
				}
			else {
					
				if(PV.IsMine) {
					mouseMovement = mouseMovement - 1;
					Hashtable hash = new Hashtable();
					hash.Add("itemIndex", mouseMovement);				PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
					photonView.RPC("RPC_UpdateMouseMovement2", RpcTarget.AllBuffered, mouseMovement);
					}
				}
				
			
		}
		if(consumables.Length != 0) {
			
			if(PV.IsMine) {
				mouseMovement = mouseMovement % consumables.Length;
				Hashtable hash = new Hashtable();
				hash.Add("itemIndex", mouseMovement);
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
				photonView.RPC("RPC_UpdateMouseMovement2", RpcTarget.AllBuffered, mouseMovement);
			}
		}
		if(PV.IsMine) {
			Hashtable hash = new Hashtable();
			hash.Add("itemIndex", mouseMovement);
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
			photonView.RPC("RPC_UpdateMouseMovement2", RpcTarget.AllBuffered, mouseMovement);
		}
		activeItem = consumables[mouseMovement];
		
		
	}

	

	//photonView.RPC("RPC_EnableGun2", RpcTarget.AllBuffered);
	photonView.RPC("RPC_DisableGuns2", RpcTarget.AllBuffered);
	if(Input.GetMouseButtonUp(1)) {
		canThrow = false;
		activeItem.GetComponent<Throwable>().Throw(time);
		time = 0;
		anim.StopPlayback();
		anim.Play("epic laer.Release Object", -1, 0f);
		
		photonView.RPC("RPC_DisableGun2", RpcTarget.AllBuffered);
		
	}
	if(Input.GetMouseButton(1) && canThrow) {
		
		rightHandWeight = 0;

		anim.Play("epic laer.Throw Object", -1, 0f);
		if(time < 1) {
			time += 0.04f;
		}
		anim.SetFloat("ThrowTime", time);
		playingAnim = true;

		activeItem.transform.position = itemPos.transform.position;

	}
	else {
		if(anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 1) {
			if(rightHandWeight < 1) {
				rightHandWeight += 0.008f;
			}
			playingAnim = false;
			anim.Play("epic laer.New State", -1, 0f);
			if(rightHandWeight >= 0.75f) {
				photonView.RPC("RPC_EnableGun2", RpcTarget.AllBuffered);
			} 
			canThrow = true;
		}
		
		//playingAnim = false;
		
	
	if(activeItem.GetComponent<Weapon>().getName() == "Grenade") {
		//oldPos.localPosition = new Vector3(0.36f, -0.07f, 1.42f);
		//oldPos.LookAt(hitPosition + new Vector3(-90, 0, 0));
		
		//oldPos.localRotation = Quaternion.Euler(0, -90.8f, 2f);
		//gun.SetActive(true);
		rightHandObj = activeItem.transform.Find("RightHandPos");
		leftHandObj = activeItem.transform.Find("LeftHandPos");
		leftHandWeight = 0;
			
			if(PV.IsMine) {
				
				activeItem.transform.localPosition =  new Vector3(0.7f, -0.035f, 1.435f);
	    			//activeItem.transform.rotation = oldPos.rotation;
				
			}
        	
	}
	
	if(activeItem.GetComponent<Weapon>().getName() == "Smoke Grenade") {
		
		//oldPos.localPosition = new Vector3(0.506f, -0.542f, 1.104f);
		//oldPos.localRotation = Quaternion.Euler(-0.25f, 0f, 0);
		//gun.SetActive(true);
		rightHandObj = activeItem.transform.Find("RightHandPos");
		leftHandObj = activeItem.transform.Find("LeftHandPos");
		leftHandWeight = 0;
			
			if(PV.IsMine) {
				
				activeItem.transform.localPosition =  new Vector3(0.7f, -0.035f, 1.435f);
	    			//activeItem.transform.rotation = oldPos.rotation;
				
			}	
        	
	}
	}



}
	
    }
    private void OnAnimatorIK(int layerIndex)
    {
	if(toggle && !playingAnim) {
	Debug.Log(rightHandObj);
	if (anim)
        {
            #region RIGHT HAND IK

            if (rightHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            }

            if(rightHandHint != null)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
                anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightHandHint.position);
            }

            #endregion
	    
            #region LEFT HAND IK

            if (leftHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }

            if (leftHandHint != null)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
                anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftHandHint.position);
            }
	    
            #endregion
	    
        }

	}
    }
    [PunRPC]
    public void RPC_UpdateMouseMovement2(int mm) {
	
	mouseMovement = mm;
    }
    [PunRPC]
    public void RPC_EnableGun2() {
	//if(toggle != true) {
	 	activeItem.SetActive(true);
	//}	
	
    }
    [PunRPC]
    public void RPC_DisableGuns2() {
	
	   foreach(GameObject g in consumables) {
		
		if(g != activeItem) {
			g.SetActive(false);
			
			
		}
	}
    }
     [PunRPC]
    public void RPC_DisableGun2() {
	//if(toggle != true) {
	 	activeItem.SetActive(false);
	//}	
	
    }

     public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
	{
		
		if(!photonView.IsMine && targetPlayer == photonView.Owner)
		{
			if(changedProps.ContainsKey("itemIndex")) {
				mouseMovement = (int)changedProps["itemIndex"];
			}
		
		}
		
	}
}
