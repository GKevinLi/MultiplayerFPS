using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class UsernameDisplay : MonoBehaviour
{
    public BetterMovement b;
    [SerializeField] PhotonView PV;
    [SerializeField] TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
	if(PV.IsMine) {
		gameObject.SetActive(false);
	}
     	text.text = PV.Owner.NickName;   
    }

    // Update is called once per frame
    void Update()
    {
	
    }
}
