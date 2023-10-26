using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class IKController : MonoBehaviourPunCallbacks
{
    private Quaternion server_arm;
    private Quaternion server_forearm;
    private Quaternion server_hand;
    private Quaternion server_Leftarm;
    private Quaternion server_Leftforearm;
    private Quaternion server_Lefthand;
    private Vector3 server_armPos;
    private Vector3 server_forearmPos;
    private Vector3 server_handPos;
    private Vector3 server_LeftarmPos;
    private Vector3 server_LeftforearmPos;
    private Vector3 server_LefthandPos;
    [SerializeField] private Transform arm;
    [SerializeField] private Transform forearm;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform Leftarm;
    [SerializeField] private Transform Leftforearm;
    [SerializeField] private Transform Lefthand;

    private Quaternion server_gun;
    private Vector3 server_gunPos;
    private GameObject server_gunObject;

    public GunBob gg;

    private List<string> aimList;
    public GameObject forearm1;
    public GameObject hand1;
    public static IKController instance;
    public bool isRightClick;
    public Transform gunPos;
    public Transform oldPos;
    public GameObject hitpoint;
    public string[] gunStrings;
    public string[] gunStrings2;
    public GameObject[] guns;
    public GameObject gun;
    public int mouseMovement = 1;
    public Animator anim;
    // Start is called before the first frame update

    [Header("Right Hand IK")]
    [Range(0, 1)] public float rightHandWeight;
    private Transform rightHandObj = null;
    public Transform rightHandHint = null;
    public Transform containerPosition = null;

    PhotonView PV;

    public GameObject head;

    public bool toggle;

    [Header("Left Hand IK")]
    [Range(0, 1)] public float leftHandWeight;
    private Transform leftHandObj = null;
    public Transform leftHandHint = null;
    public GameObject user;
    private float oldSpread;
    private Gun g;
    public Camera c;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
	PV = GetComponentInParent<PhotonView>();
	PV.Owner.TagObject = user;
	toggle = false;
	//Debug.Log(gunStrings);
	if(PV.IsMine && gunStrings.Length != 1) {
		PV.RPC("RPC_SetUpGuns", RpcTarget.AllBuffered, gunStrings);
	}
	else if(PV.IsMine){
		PV.RPC("RPC_SetUpGuns2", RpcTarget.AllBuffered, gunStrings[0]);
	}
	
	

	
	Weapon[] things = gameObject.GetComponentsInChildren<Weapon>();
	
	foreach(Weapon g in things) {
		
		g.gameObject.SetActive(false);
		
	}
	//Debug.Log(guns.Length);


	aimList = new List<string>();
        anim = GetComponent<Animator>();
	
	if(guns.Length != 0) {
		gun = guns[mouseMovement];
	}
	if(gun.GetComponent<Weapon>().getName() == "Pistol") {
		oldPos.localPosition = new Vector3(0.36f, -0.07f, 1.42f);
				
		oldPos.localRotation = Quaternion.Euler(0, -90.8f, 2f);
		gg.midPoint = oldPos.localPosition;
	}
        if(gun.GetComponent<Weapon>().getName() == "Assault Rifle") {
		
		oldPos.localPosition = new Vector3(-0.162f, 0.19f, 0.638f);
		oldPos.localRotation = Quaternion.Euler(-0.181f, -93.602f, -0.443f);
		gg.midPoint = oldPos.localPosition;
	}
	if(gun.GetComponent<Weapon>().getName() == "Sniper Rifle") {
		
		oldPos.localPosition = new Vector3(-0.162f, 0.19f, 0.638f);
		oldPos.localRotation = Quaternion.Euler(-0.181f, -93.602f, -0.443f);
		gg.midPoint = oldPos.localPosition;
	}
	if(gun.GetComponent<Weapon>().getName() == "Rocket Launcher") {
		
		oldPos.localPosition = new Vector3(-0.052f, 0f, 0.399f);
		oldPos.localRotation = Quaternion.Euler(0f, -93.302f, -0.0f);
		gg.midPoint = oldPos.localPosition;
	}
	rightHandObj = gun.transform.Find("RightHandPos");
	leftHandObj = gun.transform.Find("LeftHandPos");
	g = gun.GetComponent<Gun>();
	oldSpread = g.bulletSpread;
	
    }
    private void LateUpdate()
    {
	//if(photonView.IsMine) {
		//server_gunObject = gun;
	//}
        if (!photonView.IsMine)
        {
	    
            arm.localRotation = server_arm;
            forearm.localRotation = server_forearm;
	    hand.localRotation = server_hand;
            Leftarm.localRotation = server_Leftarm;
            Leftforearm.localRotation = server_Leftforearm;
	    Lefthand.localRotation = server_Lefthand;
	    arm.localPosition = server_armPos;
            forearm.localPosition = server_forearmPos;
	    hand.localPosition = server_handPos;
            Leftarm.localPosition = server_LeftarmPos;
            Leftforearm.localPosition = server_LeftforearmPos;
	    Lefthand.localPosition = server_LefthandPos;
	    //gun = server_gunObject;
	   
	    //if(gun != null) {
	    	//gun.transform.localRotation = server_gun;
	    	//gun.transform.localPosition = server_gunPos;
		
		//gun.SetActive(true);
	    //}
	    
	    
	    
	
        }
    }
    private void FixedUpdate()
    {
        if (photonView.IsMine == true)
        {
            photonView.RPC("RPC_UpdateBoneRotations", RpcTarget.AllBuffered, arm.localRotation, forearm.localRotation, hand.localRotation, Leftarm.localRotation, Leftforearm.localRotation, Lefthand.localRotation, arm.localPosition, forearm.localPosition, hand.localPosition, Leftarm.localPosition, Leftforearm.localPosition, Lefthand.localPosition);
        }
	if (photonView.IsMine == true)
        {
            //photonView.RPC("RPC_UpdateGunPositions", RpcTarget.AllBuffered, gun.transform.localRotation, gun.transform.localPosition);
        }
    }
    [PunRPC]
    private void RPC_UpdateBoneRotations(Quaternion h, Quaternion j, Quaternion k, Quaternion a, Quaternion b, Quaternion c, Vector3 s, Vector3 t,Vector3 u,Vector3 x,Vector3 y,Vector3 z)
    {
        server_arm = h;
        server_forearm = j;
	server_hand = k;
	server_Leftarm = a;
        server_Leftforearm = b;
	server_Lefthand = c;
	server_armPos = s;
        server_forearmPos = t;
	server_handPos = u;
	server_LeftarmPos= x;
        server_LeftforearmPos = y;
	server_LefthandPos = z;
    }
    [PunRPC]
    private void RPC_UpdateGunPositions(Quaternion a, Vector3 b) {
	server_gun = a;
	server_gunPos = b;
	
	server_gunObject = gun;
	
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) {
		toggle = !toggle;
		
	}
	
	
        //Debug.Log(isRightClick);
	leftHandWeight = 0;
	if(Input.GetAxisRaw("Mouse ScrollWheel") != 0) {
		if(Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
  			//if(PV.IsMine) {
				//mouseMovement = mouseMovement + 1;
				if(PV.IsMine) {
					mouseMovement = mouseMovement + 1;
					Hashtable hash = new Hashtable();
					hash.Add("itemIndex", mouseMovement);
						PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
						PV.RPC("RPC_UpdateMouseMovement", RpcTarget.AllBuffered, mouseMovement);
					}
			//}
		}
		if(Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
			//if(PV.IsMine) {
				if(mouseMovement == 0) { 
					if(PV.IsMine) {
						mouseMovement = guns.Length -1;
						Hashtable hash = new Hashtable();
						hash.Add("itemIndex", mouseMovement);
						PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
						PV.RPC("RPC_UpdateMouseMovement", RpcTarget.AllBuffered, mouseMovement);
					}
				}
				else {
					
					if(PV.IsMine) {
						mouseMovement = mouseMovement - 1;
						Hashtable hash = new Hashtable();
						hash.Add("itemIndex", mouseMovement);
						PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
						PV.RPC("RPC_UpdateMouseMovement", RpcTarget.AllBuffered, mouseMovement);
					}
				}
				
			//}
		}
		if(guns.Length != 0) {
			
			if(PV.IsMine) {
				mouseMovement = mouseMovement % guns.Length;
				Hashtable hash = new Hashtable();
				hash.Add("itemIndex", mouseMovement);
				PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
				PV.RPC("RPC_UpdateMouseMovement", RpcTarget.AllBuffered, mouseMovement);
			}
		}
		if(PV.IsMine) {
			Hashtable hash = new Hashtable();
			hash.Add("itemIndex", mouseMovement);
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
			PV.RPC("RPC_UpdateMouseMovement", RpcTarget.AllBuffered, mouseMovement);
		}
		
		
		
		if(guns.Length != 0) {
			g = guns[mouseMovement].GetComponent<Gun>();
		}
		if(g != null) {
			oldSpread = g.bulletSpread;
		}
		
		
	}
	if(PV.IsMine) {
       	 	PV.RPC("RPC_UpdateMouseMovement", RpcTarget.AllBuffered, mouseMovement);
	}
	if(guns.Length != 0) {
		if(mouseMovement < guns.Length) {
			gun = guns[mouseMovement];
			
		}
		
		
	}
	
	 if(!photonView.IsMine) {
		Debug.Log(gun.activeSelf);
		return;
	}
	if(Input.GetAxisRaw("Mouse ScrollWheel") != 0) {
	if(gun.GetComponent<Weapon>().getName() == "Pistol") {
		oldPos.localPosition = new Vector3(0.36f, -0.07f, 1.42f);
				
		oldPos.localRotation = Quaternion.Euler(0, -90.8f, 2f);
		gg.midPoint = oldPos.localPosition;
	}
        if(gun.GetComponent<Weapon>().getName() == "Assault Rifle") {
		
		oldPos.localPosition = new Vector3(-0.162f, 0.19f, 0.638f);
		oldPos.localRotation = Quaternion.Euler(-0.181f, -93.602f, -0.443f);
		gg.midPoint = oldPos.localPosition;
	}
	if(gun.GetComponent<Weapon>().getName() == "Sniper Rifle") {
		
		oldPos.localPosition = new Vector3(-0.162f, 0.19f, 0.638f);
		oldPos.localRotation = Quaternion.Euler(-0.181f, -93.602f, -0.443f);
		gg.midPoint = oldPos.localPosition;
	}
	if(gun.GetComponent<Weapon>().getName() == "Rocket Launcher") {
		
		oldPos.localPosition = new Vector3(-0.052f, 0f, 0.399f);
		oldPos.localRotation = Quaternion.Euler(0f, -93.302f, -0.0f);
		gg.midPoint = oldPos.localPosition;
	}
	}
	//PV.RPC("RPC_DisableGun", RpcTarget.AllBuffered);
	//if(toggle != true) {
		PV.RPC("RPC_EnableGun", RpcTarget.AllBuffered);
	//}
	PV.RPC("RPC_DisableGuns", RpcTarget.AllBuffered);
	if(toggle == true) {
		PV.RPC("RPC_DisableGun", RpcTarget.AllBuffered);
		return;
	}
	Ray ray = new Ray(gun.GetComponent<Gun>().bulletSpawnPos.position, Camera.main.transform.forward);
	
	RaycastHit hit;
	
	Physics.Raycast(ray, out hit, 1000f); 
	Vector3 hitPosition = hit.point;
	//hitpoint.transform.position = hit.point;
	if(gun.GetComponent<Weapon>().getName() == "Pistol") {
		//oldPos.localPosition = new Vector3(0.36f, -0.07f, 1.42f);
		//oldPos.LookAt(hitPosition + new Vector3(-90, 0, 0));
		
		//oldPos.localRotation = Quaternion.Euler(0, -90.8f, 2f);
		//gun.SetActive(true);
		rightHandObj = gun.transform.Find("RightHandPos");
		leftHandObj = gun.transform.Find("LeftHandPos");
        	
	}
	
	if(gun.GetComponent<Weapon>().getName() == "Assault Rifle") {
		
		//oldPos.localPosition = new Vector3(0.506f, -0.542f, 1.104f);
		//oldPos.localRotation = Quaternion.Euler(-0.25f, 0f, 0);
		//gun.SetActive(true);
		rightHandObj = gun.transform.Find("RightHandPos");
		leftHandObj = gun.transform.Find("LeftHandPos");
        	
	}
	if(gun.GetComponent<Weapon>().getName() == "Sniper Rifle") {
		
		//oldPos.localPosition = new Vector3(0.506f, -0.542f, 1.104f);
		//oldPos.localRotation = Quaternion.Euler(-0.0f, -0.7f, 0);
		//gun.SetActive(true);
		rightHandObj = gun.transform.Find("RightHandPos");
		leftHandObj = gun.transform.Find("LeftHandPos");
        	
	}
	if(gun.GetComponent<Weapon>().getName() == "Rocket Launcher") {
		
		//oldPos.localPosition = new Vector3(1.06f, -0.542f, 1.104f);
		//oldPos.localRotation = Quaternion.Euler(-0.0f, -0.7f, 0);
		//gun.SetActive(true);
		rightHandObj = gun.transform.Find("RightHandPos");
		leftHandObj = gun.transform.Find("LeftHandPos");
        	
	}
	if(gun.GetComponent<Weapon>().getName() == "Epic Sword") {
		rightHandObj = gun.transform.Find("RightHandPos");
		leftHandObj = gun.transform.Find("LeftHandPos");
		oldPos.localPosition = new Vector3(0.577f, -0.792f, 1.251f);
		oldPos.localRotation = Quaternion.Euler(0, 0, 0);
	}
	else {
		
		rightHandWeight = 0;
		leftHandWeight = 0;
	}












	if (Input.GetMouseButtonDown(1) && photonView.IsMine) {
		aimList.Add(PV.Owner.NickName);
		
	}
	if (Input.GetMouseButtonUp(1) && photonView.IsMine) {
		
		
	}
	/*
	Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
	RaycastHit hit;
	
	Physics.Raycast(ray, out hit, 1000f);
	float distanceToHit = Vector3.Distance(Camera.main.transform.position, hit.point);
	*/
	//hitpoint.transform.position = hit.point;
	
	
	if (Input.GetMouseButton(1)) {
		if(c != null) {
			c.fieldOfView = 30f;
		}
		
		
		
		if(g != null) {
			g.bulletSpread = oldSpread / 3f;
		}

		
		if(gun.GetComponent<Weapon>().getName() == "Assault Rifle") {
			if(PV.IsMine) {
	    			gun.transform.localPosition =  new Vector3(-0.428f, 0.196f, 0.945f);
				gun.transform.localRotation = Quaternion.Euler(-0.16f, -90.981f, 1.906f);
	    			gun.transform.parent.rotation = head.transform.rotation;
			}
			
			PV.RPC("RPC_SyncLeftHandWeight", RpcTarget.All, 1);
			
		}
		if(gun.GetComponent<Weapon>().getName() == "Sniper Rifle") {
			c.fieldOfView = 10f;
			
			if(PV.IsMine) {
				gun.transform.position =  gunPos.position;
	    			gun.transform.rotation = gunPos.rotation;
			}
			
			PV.RPC("RPC_SyncLeftHandWeight", RpcTarget.All, 1);
			
		}
		if(gun.GetComponent<Weapon>().getName() == "Rocket Launcher") {

		}
		else {
			if(PV.IsMine) {
				gun.transform.position =  gunPos.position;
	    			gun.transform.rotation = gunPos.rotation;
			}
			PV.RPC("RPC_SyncLeftHandWeight", RpcTarget.All, 1);
		}
	
	}
	
	else {
		
		if(c != null) {
			c.fieldOfView = 60f;
			Camera.main.transform.position = gameObject.transform.Find("Head/CameraPos").position;
		}
		
		if(g != null) {
			g.bulletSpread = oldSpread;
		}
	    	if(gun.GetComponent<Weapon>().getName() == "Pistol") {
			PV.RPC("RPC_SyncLeftHandWeight", RpcTarget.All, 0);
			
			if(PV.IsMine) {
				
				gun.transform.position =  oldPos.position;
	    			gun.transform.rotation = oldPos.rotation;
				
			}			
		}
		else if(gun.GetComponent<Weapon>().getName() == "Epic Sword") {
			PV.RPC("RPC_SyncLeftHandWeight", RpcTarget.All, 0);
			if(PV.IsMine) {
				gun.transform.parent.position =  oldPos.position;
	    			gun.transform.parent.rotation = oldPos.rotation;
			}
		}
		else if(gun.GetComponent<Weapon>().getName() == "Rocket Launcher") {
			PV.RPC("RPC_SyncLeftHandWeight", RpcTarget.All, 1);
			gun.transform.localPosition = oldPos.localPosition;
			gun.transform.localRotation = oldPos.localRotation;
	    		gun.transform.parent.rotation = head.transform.rotation;

		}
		else {
			
			PV.RPC("RPC_SyncLeftHandWeight", RpcTarget.All, 1);
			if(PV.IsMine) {
				
				gun.transform.localPosition = oldPos.localPosition;
				gun.transform.localRotation = oldPos.localRotation;
	    			gun.transform.parent.rotation = head.transform.rotation;
				
			}
			
		}
		
	}
	rightHandWeight = 1;
	//gun.transform.localRotation = new Quaternion(0,-90,0,0);
    }

















    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
	{
		
		if(!photonView.IsMine && targetPlayer == photonView.Owner)
		{
			if(changedProps.ContainsKey("itemIndex")) {
				mouseMovement = (int)changedProps["itemIndex"];
			}
			if(changedProps.ContainsKey("leftHandWeight")) {
				leftHandWeight = (int)changedProps["leftHandWeight"];
			}
		}
		
	}

     private void OnAnimatorIK(int layerIndex)
    {
	if(anim == null) {
		return;
	}
	if(PV == null) {
		return;
	}	
	if(!PV.IsMine) {
		return;
	}
	if(toggle == true) {
		return;
	}
	//Debug.Log(leftHandWeight);
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
    
    [PunRPC]
    public void RPC_SyncLeftHandWeight(int g) {
	   leftHandWeight = g;
    }
    [PunRPC]
    public void RPC_Gun() {
	
	
	   foreach(GameObject g in guns) {
		
		if(g == gun) {
			g.SetActive(true);
			//Debug.Log("activating" + g);
			
		}
		else {
			g.SetActive(false);
			//Debug.Log("deactivating" + g);
		}
	}
	
    }
    [PunRPC]
    public void RPC_DisableGun() {
	    
	   gun.SetActive(false);
	   
    }
    [PunRPC]
    public void RPC_EnableGun() {
	if(toggle != true) {
	 	gun.SetActive(true);
	}	
	
    }
    [PunRPC]
    public void RPC_DisableGuns() {
	
	   foreach(GameObject g in guns) {
		
		if(g != gun) {
			g.SetActive(false);
			
			
		}
	}
    }
    [PunRPC]
    public void RPC_SetUpGuns(string[] gunString) {
        gunStrings2 = gunString;
	guns = new GameObject[gunStrings2.Length];
	for(int i = 0; i < gunStrings2.Length; i++) {
		Transform t = RecursiveFindChild(gameObject.transform, gunStrings2[i]);
		if(t != null) {
			guns[i] = t.gameObject;
		}	
	}
    }
    [PunRPC]
    public void RPC_SetUpGuns2(string gun) {
        string[] gunString = new string[1];
	gunString[0] = gun;
        gunStrings2 = gunString;
	guns = new GameObject[gunStrings2.Length];
	for(int i = 0; i < gunStrings2.Length; i++) {
		Transform t = RecursiveFindChild(gameObject.transform, gunStrings2[i]);
		if(t != null) {
			guns[i] = t.gameObject;
		}	
	}
    }
    [PunRPC]
    public void RPC_UpdateMouseMovement(int mm) {
	
	mouseMovement = mm;
    }
    public void setWeight(int a) {
	leftHandWeight  = a;
    }
    Transform RecursiveFindChild(Transform parent, string childName)
{
    foreach (Transform child in parent)
    {
        if(child.name == childName)
        {
            return child;
        }
        else
        {
            Transform found = RecursiveFindChild(child, childName);
            if (found != null)
            {
                    return found;
            }
        }
    }
    return null;
}
    
    
    
    
}
