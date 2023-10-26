using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Weapon : MonoBehaviourPun
{
    public string name;
    public float reloadTime;
    public int energyyCost;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getCost() {
	return energyyCost;
    }
    public string getName() {
	return name;
    }
}
