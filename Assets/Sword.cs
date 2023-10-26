using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Sword : Weapon
{
    private Animator anim;
    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
	if(!PV.IsMine) {
		return;
	}
        if (Input.GetMouseButtonDown(0)) {
		photonView.RPC("RPC_Swing", RpcTarget.All);
		
	}
	
    }
    [PunRPC]
    void RPC_Swing() {
	int num = Random.Range(1,3);
		if(num == 1) {
			anim.Play("Sword Attack 2", -1, 0f);
		}
		if(num == 2) {
			anim.Play("Sword Attack 1", -1, 0f);
		}
    }
}
