using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BetterMovement : MonoBehaviour, IDamageable
{
    public GameObject name;
    public Animator anim;
    public Transform thing;
    public GameObject ui;
    [SerializeField] TMP_Text healthtext;
    public RectTransform healthBar;
    public LayerMask layerMask;
    float velocityZ = 0;
    float velocityX = 0;
    public float acceleration = 2.0f;
    public float sideacceleration = 5.0f;
    public float deceleration = 5.0f;
    public float sidedeceleration = 20.0f;
    private float crouchMultiplier = 1f;
    public float accelLimit = 2.0f;
    public int jumpnum = 1;
    public Rigidbody rb;
    

    float maxHealth = 100f;
    float currentHealth = 100f;
    public float maxEnergy = 100f;
    public float currentEnergy = 100f;
    PhotonView PV;
    
    [SerializeField] TMP_Text energytext;
    public RectTransform energyBar;



    PlayerManager playerManager;
    // Start is called before the first frame update
    void Awake() {
	
	PV = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    void Start()
    {
        if(!PV.IsMine) {
		Destroy(GetComponentInChildren<Camera>().gameObject);
		//Destroy(rb);
		Destroy(ui);
		
	}
	healthtext.text = "100";
	energytext.text = "100";
        InvokeRepeating("changeEnergy", 0.25f, 0.25f);	
    }
   
    // Update is called once per frame
   
    void Update()
    {
	  if(!PV.IsMine) {
		return;
	  }
	  if(Input.GetKeyDown(KeyCode.Space)) {
		if(jumpnum >= 1) {
			anim.ResetTrigger("Unjump");
			anim.Play("Base Layer.Jumping", -1, 0f);
			rb.isKinematic = false;
			gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 500f);
			jumpnum--;
		}
	  }
	  
        
	  
	
	
	  if(Input.GetKeyDown(KeyCode.LeftShift)) {
		
		anim.SetTrigger("Crouch");
		anim.ResetTrigger("Uncrouch");
		crouchMultiplier = 0.2f;
		accelLimit = 0.8f;
	  }
	  else if(Input.GetKeyUp(KeyCode.LeftShift)){
		anim.SetTrigger("Uncrouch");
		anim.ResetTrigger("Crouch");
		crouchMultiplier = 1f;
		accelLimit = 2.0f;
	  }
	  
        transform.rotation = thing.rotation;
	  bool forwardPressed = Input.GetKey("w");
	  bool leftPressed = Input.GetKey("a");
	  bool rightPressed = Input.GetKey("d");
	  bool backPressed = Input.GetKey("s");
	  
	  if(forwardPressed && velocityZ < 2.0f) {
		velocityZ += Time.deltaTime * acceleration;
	  }
	  if(backPressed && velocityZ > -0.5f) {
		velocityZ -= Time.deltaTime * acceleration;
	  }
	  if(leftPressed && velocityX > -2.0f) {
		velocityX -= Time.deltaTime * acceleration;
		
	  }
	  if(rightPressed && velocityX < 2.50) {
		velocityX += Time.deltaTime * acceleration;
	  }
	  if(!forwardPressed && velocityZ > 0.0f) {
		velocityZ -= Time.deltaTime * deceleration;
	  }
	  if(!backPressed && velocityZ < 0.0f) {
		velocityZ += Time.deltaTime * deceleration;
	  }
	  if(!rightPressed && velocityX > 0.0f) {
		velocityX -= Time.deltaTime * deceleration;
	  }
	  if(!leftPressed && velocityX < 0.0f) {
		velocityX += Time.deltaTime * deceleration;
	  }
	  if(!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f)) {
		velocityX = 0.0f;
	  }
	  if(!forwardPressed && !backPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f)) {
		velocityZ = 0.0f;
	  }
	  //transform.Translate(new Vector3(velocityX * 0.02f * crouchMultiplier, 0, velocityZ * 0.02f * crouchMultiplier));
	  
	  //transform.Translate(velocityX * thing.forward * 0.01f);
	  
	  anim.SetFloat("Velocity X", velocityX);
	  anim.SetFloat("Velocity Z", velocityZ);
	  //Debug.Log("VelocityX" + velocityX);
	  //Debug.Log("VelocityZ" + velocityZ);
  	  if(transform.position.y < -10f) {
		Die();
	  }
    }
    
   
     void FixedUpdate() {
	if(!PV.IsMine) {
		return;
	}
	
	Ray rightLegDist = new Ray(thing.position, Vector3.up);
	Debug.DrawRay(thing.position, Vector3.up, Color.yellow);
	RaycastHit RR;  
	
	if(!(Physics.Raycast(rightLegDist, out RR, 1.2f, layerMask))) {
		
		
	  //Debug.Log(RR.transform.gameObject);
		
	  rb.MovePosition(transform.position + new Vector3(velocityZ * 0.015f * crouchMultiplier * transform.forward.x, 0, velocityZ * 0.015f * crouchMultiplier * transform.forward.z) + new Vector3(velocityX * 0.015f * crouchMultiplier * transform.right.x, 0, velocityX * 0.015f * crouchMultiplier * transform.right.z));
	}
	else {	
	   rb.MovePosition(transform.position + new Vector3(transform.forward.x * -0.1f, 0, transform.forward.z * -0.1f));
	}
    }
    public void TakeDamage(float damage) {
	if(!PV.IsMine) {
		//Debug.Log("returned3333");
		return;
		
	}
	PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
	
   }
    [PunRPC]
    void RPC_TakeDamage(float damage) {
	if(!PV.IsMine) {
		//Debug.Log("returned");
		return;
		
	}
        currentHealth -= damage;
	Debug.Log(currentHealth);
	healthBar.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
	healthtext.text = currentHealth + "";
        if(currentHealth <= 0) {
		Die();
	}
    }
    public void ChangeName(bool b, PhotonView PV) {
	//PV.RPC("RPC_ChangeName", PV, b);
    }
    [PunRPC]
    void RPC_ChangeName(bool b) {
	
	name.SetActive(b);
	
    }
    
    void Die() {
	playerManager.Die();
    }
    public void changeEnergy() {
	if(!PV.IsMine) {
		return;
	}
	if(currentEnergy < 100) {
		currentEnergy += 1;
	}
        changeEnergyBar();
    }
    public void changeEnergyBar() {
	if(!PV.IsMine) {
		return;
	}
	energyBar.localScale = new Vector3(currentEnergy / maxEnergy, 1, 1);
	energytext.text = currentEnergy + "";
    }
   

}
