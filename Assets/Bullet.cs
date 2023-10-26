using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Gun gunShotFrom;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start() {
	
    }
    // Update is called once per frame
    void Update()
    {
	
	
        if(transform.position.y < -10f) {
		Destroy(gameObject);
	}
	
    }
    void OnTriggerStay(Collider collision) {
        
        GameObject g = collision.gameObject;
	while(g != null && g.transform.parent != null) {
		g = g.transform.parent.gameObject;
	}
	
        //Vector3 v = collision.ClosestPointOnBounds(transform.position);
	
	
	//gunShotFrom.bulletHit(v);
        gameObject.SetActive(false);
    }
    void OnCollisionEnter(Collision collision) {
	
	gameObject.SetActive(false);
	GameObject g = collision.gameObject;
	Debug.Log("Ping!");
	g.GetComponent<IDamageable>()?.TakeDamage(gunShotFrom.damage);
	Vector3 v = collision.contacts[0].point;
	gunShotFrom.bulletHit(v, collision.GetContact(0).normal);
    }
}
