using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateHead : MonoBehaviour
{
    public Transform thing;
    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
	if(!PV.IsMine) {
			return;
		}
        transform.rotation = thing.rotation;
	
    }
}
