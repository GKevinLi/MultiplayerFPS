using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Throwable : Weapon
{
    public GameObject bullet;
    public GameObject gunUser;
    public GameObject cam;
    public int damage;
    int energyCost = 0;
    public int numCharges;
    public PhotonView PV;
    public int projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        energyCost = base.getCost();
    }
    public string getWeaponName() {
	return base.getName();
    }
    // Update is called once per frame
    
    void Update()
    {
        
    }
    public void Throw(float spd) {
	photonView.RPC("RPC_Throw", RpcTarget.All, spd);
    }
    [PunRPC]
    void RPC_Throw(float spd) {
	GameObject temp = Instantiate(bullet);
	temp.SetActive(true);
	temp.GetComponent<ThrownItem>().gunShotFrom = gameObject.GetComponent<Throwable>();
	temp.transform.position = transform.position;

	Vector3 newForce = cam.transform.forward;
	temp.GetComponent<Rigidbody>().velocity = newForce * (spd * 12);

    }
}
