using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Gun gunShotFrom;
    public GameObject burst;
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
	
	//Debug.Log("Ping2!");
	//gunShotFrom.bulletHit(v);
        gameObject.SetActive(false);
    }
    void OnCollisionEnter(Collision collision) {
	GameObject newBurst = Instantiate(burst, collision.contacts[0].point, transform.rotation);
	newBurst.SetActive(true);
	newBurst.GetComponent<ParticleSystem>().Play();
	Collider[] things = Physics.OverlapSphere(collision.contacts[0].point, 3);
	HashSet<UnityEngine.GameObject> test = new HashSet<UnityEngine.GameObject>();
	foreach(Collider c in things) {
		GameObject g = c.transform.root.gameObject;
		test.Add(g);
	}
	gameObject.SetActive(false);
	foreach(GameObject c in test) {
		
		//GameObject g = c.transform.root.gameObject;

		Debug.Log(c);
		c.GetComponent<IDamageable>()?.TakeDamage(gunShotFrom.damage);
		if(c.GetComponent<Rigidbody>() != null) {
			c.GetComponent<Rigidbody>().AddExplosionForce(800, collision.contacts[0].point, 150, 3);
		}
	}
	
	//Debug.Log("Ping!");
	
	Vector3 v = collision.contacts[0].point;
	gunShotFrom.bulletHit(v, collision.GetContact(0).normal);
    }
    public static string[] RemoveDuplicates(string[] s)  {
   	 HashSet<string> set = new HashSet<string>(s);
   	 string[] result = new string[set.Count];
    	 set.CopyTo(result);
   	 return result;
    }
}