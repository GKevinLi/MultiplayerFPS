using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    BetterMovement playerController;
    void Awake()
	{
		playerController = transform.parent.gameObject.GetComponentInParent<BetterMovement>();
	}
    void Start()
    {
        
    }

  void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == playerController.gameObject)
			return;
		playerController.jumpnum = 1;
		playerController.anim.SetTrigger("Unjump");
		
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == playerController.gameObject)
			return;
		playerController.jumpnum = 0;
		
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject == playerController.gameObject)
			return;

		playerController.jumpnum = 1;
		playerController.anim.SetTrigger("Unjump");
	}
}
