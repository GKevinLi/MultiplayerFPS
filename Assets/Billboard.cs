using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera[] camArray;
    Camera cam;
    // Update is called once per frame
    void Update()
    {
        if(cam == null) {
		camArray = FindObjectsOfType<Camera>();
		foreach(Camera c in camArray) {
			if(!(c.gameObject.tag == "bruh")) {
				cam = c;
			}
		}
	}
	if(cam == null) {
		return;
	}
	transform.LookAt(cam.transform);
    }
}
