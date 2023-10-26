using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
public class MenuLogic : MonoBehaviour
{
    //public PhotonView PV;
    
    public GameObject camera;
    public GameObject spawns;
    public List<string> gunList;
    public GameObject[] buttons;
    public GameObject[] menus;
    public GameObject selected;
    
    // Start is called before the first frame update
    void Start()
    {
        selectButton(buttons[0]);
	gunList = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
	
       
    }
    public void spawnPlayer() {
	if(gunList.Count >= 1 && gunList.Count <= 3) {
		gameObject.SetActive(false);
		
		camera.SetActive(false);
		spawns.SetActive(true);
		GameObject pm = PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
		pm.GetComponent<PlayerManager>().gunList = gunList.ToArray();
		pm.GetComponent<PlayerManager>().obj = gameObject;
		pm.GetComponent<PlayerManager>().cam = camera;
	}
    }
    public void selectButton(GameObject b) {
	gunList = new List<string>();
	selected = b;
	for(int i = 0; i < buttons.Length; i++) {
		GameObject g = buttons[i];
		if(g == selected) {
			var colors = g.GetComponent<Button>().colors;
			menus[i].SetActive(true);
			colors.normalColor = new Color(colors.normalColor[0],colors.normalColor[1],colors.normalColor[2],1f);
			g.GetComponent<Button>().colors = colors;
		}
		else {
			menus[i].SetActive(false);
			var colors = g.GetComponent<Button>().colors;
			colors.normalColor = new Color(colors.normalColor[0],colors.normalColor[1],colors.normalColor[2],0.2f);
			g.GetComponent<Button>().colors = colors;
		}
	}
    }
}
