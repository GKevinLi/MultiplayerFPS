using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    public Vector3 oldPos;
    public Vector3 recoilPos;
    float cnt;
    float pause;
    private bool seq;
    private bool alt;
    // Start is called before the first frame update
    void Start()
    {
        cnt = 0;
	alt = false;
	pause = 1;
	seq = false;
    }

    // Update is called once per frame
    void Update()
    {
        //pause += 0.1f;
        
	if(seq) {
		transform.localPosition = Vector3.Lerp(oldPos, recoilPos, cnt);
		if(!alt) {
			cnt += 0.07f;
			if(cnt >= 1) {
				cnt = 1;
				//if(!Input.GetMouseButton(0)) {
				alt = true;
//}
			}
		}
		if(alt) {
			cnt -= 0.07f;
			if(cnt <= 0) {
				alt = false;
				seq = false;
				pause = 0;
			}
		}
		
	}
	else {
		oldPos = transform.localPosition;
	}
    }
    public void recoil() {
	
	if(!seq) {
		
		recoilPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.2f);
	}
	if(seq) {
		cnt += 0.25f;
		if(cnt >= 1) {
			cnt = 1;
		}
	}
	seq = true;
	
	

    }
}
