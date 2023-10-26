using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    public string[] gunList;
    public GameObject obj;
    public GameObject cam;
    GameObject controller;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Start()
    {
        if(PV.IsMine) {
		CreateController();
	}
    }
    public void CreateController() {
	Debug.Log("Instantiated Player Controller");
	Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
	controller = PhotonNetwork.Instantiate("Player", spawnpoint.position, spawnpoint.rotation, 0, new object[] {PV.ViewID});
	//foreach(string s in gunList) {
		//Debug.Log(s);
	//}
	//Hashtable hash = new Hashtable();
	//hash.Add("guns", gunList);
	//controller.GetComponent<PhotonView>().Owner.SetCustomProperties(hash);
	//Debug.Log(controller.GetComponent<PhotonView>().Owner.CustomProperties["guns"]);
	controller.GetComponentInChildren<IKController>().gunStrings = gunList;
    }
    public void Die() {
	
	PhotonNetwork.Destroy(controller);
	obj.SetActive(true);
	cam.SetActive(true);
	Screen.lockCursor = false;
	Cursor.visible = true;
	obj.GetComponent<MenuLogic>().gunList.Clear();
    }
}
