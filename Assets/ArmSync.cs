using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ArmSync : MonoBehaviour
{
    public PhotonView PV;
    public GameObject arm1;
    public GameObject forearm1;
    public GameObject hand1;
    // Start is called before the first frame update
    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
	//Debug.Log(gameObject.transform);
        //PV.RPC("RPC_SyncIK", RpcTarget.All, gameObject.transform.position, forearm1.transform.position, hand1.transform.position);
    }
    
}
