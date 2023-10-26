using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Gun : Weapon
{
    public GameObject bullet;
    public GameObject gunUser;
    int energyCost = 0;
    
    public GunRecoil gunRecoil;
    public GunRecoil gunRecoil2;
    public float recoil;
    public float fireRate;
    public float projectileSpeed;
    public Transform bulletSpawnPos;
    public float bulletSpread;
    public ParticleSystem muzzleFlash;
    private float counter;
    public PhotonView PV;

    public Vector3 c;
    private Vector3 hitPoint;
    public float damage;
    public GameObject bulletImpactPrefab;

    float xDiff;
    float yDiff;
    float zDiff;
    
    public Transform rot;
    public Vector3 ation;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        energyCost = base.getCost();
    }
    public string getGunName() {
	return base.getName();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
	if(!PV.IsMine) {
		return;
	}
	
	Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 10, Color.green);
	counter = counter + 0.01f;
        if (Input.GetMouseButton(0)) {
		Ray ray = new Ray(Camera.main.transform.position, 				Camera.main.transform.forward);
	
		RaycastHit hit;
		Physics.Raycast(ray, out hit, 1000f); 
		hitPoint = hit.point;
		photonView.RPC("RPC_SyncHitPoint", RpcTarget.All, hitPoint);
		photonView.RPC("RPC_SyncTransform", RpcTarget.All, Camera.main.transform.forward);
		//rot.rotation = Camera.main.transform.rotation;
		photonView.RPC("RPC_SyncRotation", RpcTarget.All, new Vector3(Camera.main.transform.eulerAngles.z, Camera.main.transform.eulerAngles.y - 90, -Camera.main.transform.eulerAngles.x));
		float xDiff = Random.Range(-bulletSpread, bulletSpread);
		float yDiff = Random.Range(-bulletSpread, bulletSpread);
		float zDiff = Random.Range(-bulletSpread, bulletSpread);
		photonView.RPC("RPC_SyncXYZ", RpcTarget.All, xDiff, yDiff, zDiff);
	
		if(counter > fireRate && gunUser.GetComponent<BetterMovement>().currentEnergy >= energyCost) {
			gunUser.GetComponent<BetterMovement>().currentEnergy -= energyCost;
			gunUser.GetComponent<BetterMovement>().changeEnergyBar();
			if(Input.GetMouseButton(1)) {
				photonView.RPC("RPC_Shoot", RpcTarget.All, 1);
			}
			else {
				photonView.RPC("RPC_Shoot",RpcTarget.All, 0);
			}
			counter = 0;
		}
	}
    }
    public void bulletHit(Vector3 hitPosition, Vector3 hitNormal) {
	photonView.RPC("RPC_BulletImpact", RpcTarget.All, hitPosition, hitNormal);
    }
    
    [PunRPC]
    void RPC_Shoot(int rc) {
	
	GameObject newBullet = Instantiate(bullet);
	if(rc == 1) {
		gunRecoil2.recoil();
	}
	else {
		gunRecoil.recoil();
	}
	muzzleFlash.gameObject.SetActive(true);
	muzzleFlash.gameObject.transform.position = bulletSpawnPos.position;
	muzzleFlash.gameObject.transform.rotation = Quaternion.Euler(-transform.eulerAngles.z, (transform.eulerAngles.y + 90f), transform.eulerAngles.z* transform.forward.x);
	muzzleFlash.Play();

	if(newBullet.GetComponent<Bullet>() == null) {
		 newBullet.GetComponent<Rocket>().gunShotFrom = gameObject.GetComponent<Gun>();
		
	}
	else {
        	newBullet.GetComponent<Bullet>().gunShotFrom = gameObject.GetComponent<Gun>();
	}
	if(PV.IsMine) {
		Ray ray = new Ray(Camera.main.transform.position, 				Camera.main.transform.forward);
	
		RaycastHit hit;
		Physics.Raycast(ray, out hit, 1000f); 
		hitPoint = hit.point;
		photonView.RPC("RPC_SyncHitPoint", RpcTarget.All, hitPoint);
		photonView.RPC("RPC_SyncTransform", RpcTarget.All, Camera.main.transform.forward);
		photonView.RPC("RPC_SyncRotation", RpcTarget.All, new Vector3(Camera.main.transform.eulerAngles.z, Camera.main.transform.eulerAngles.y - 90, -Camera.main.transform.eulerAngles.x));
		//Debug.Log(hitPoint);
	}
	if(!PV.IsMine) {
		//Debug.Log(hitPoint);
	}
	
			muzzleFlash.gameObject.SetActive(true);
			muzzleFlash.Play();
			newBullet.transform.position = bulletSpawnPos.position;
			newBullet.SetActive(true);
			if(hitPoint.x != 0 && hitPoint.y != 0 &&hitPoint.z != 0) 			{
				newBullet.transform.LookAt(hitPoint);
				Debug.Log("am heere");
			}
			
			Vector3 newForce = new Vector3(newBullet.transform.forward.x + xDiff, newBullet.transform.forward.y + yDiff, newBullet.transform.forward.z + zDiff);
			if(hitPoint.x == 0 && hitPoint.y == 0 &&hitPoint.z == 0) {
				Debug.Log("hi");
				
				newForce = new Vector3(c.x + xDiff, c.y + yDiff, c.z + zDiff);
			}
			
			//newBullet.transform.LookAt(hitPoint);
			//newBullet.transform.rotation = Camera.main.transform.rotation;
			newBullet.transform.eulerAngles = ation;
			
			newBullet.GetComponent<Rigidbody>().velocity = (((newForce) * projectileSpeed));
			gunUser.GetComponent<Rigidbody>().AddForce(((gameObject.transform.right) * -recoil));  
			
			
    }
    [PunRPC]
    void RPC_BulletImpact(Vector3 hitPosition, Vector3 hitNormal) {
	//Debug.Log(hitPosition);
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
	if(colliders.Length != 0) {
		Quaternion q = Quaternion.LookRotation(-hitNormal, Vector3.up);
		GameObject bulletImpactObj =  Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, new Quaternion(q.x, q.y, q.z, q.w));
		Destroy(bulletImpactObj, 10f);
		bulletImpactObj.transform.SetParent(colliders[0].transform);
	}
	
    }
    [PunRPC]
    void RPC_SyncHitPoint(Vector3 hitPosition) {
	hitPoint = hitPosition;
    }
    [PunRPC]
    void RPC_SyncTransform(Vector3 transform) {
	c = transform;
    }
    [PunRPC]
    void RPC_SyncRotation(Vector3 rot) {
	ation = rot;
    }
    [PunRPC]
    void RPC_SyncXYZ(float x, float y, float z) {
	xDiff = x;
	yDiff = y;
	zDiff = z;
    }
}
